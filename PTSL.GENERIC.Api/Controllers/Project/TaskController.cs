using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController<ITaskService, TaskOfProjectVM, TaskOfProject>
    {
        private readonly ITaskService _taskService;
        private readonly ITaskLogService _taskLogService;
        private readonly ITaskTimeTrackingService _taskTimeTrackingService;
        public TaskController(ITaskService taskService, ITaskLogService taskLogService, ITaskTimeTrackingService taskTimeTrackingService)
            : base(taskService)
        {
            this._taskService = taskService;
            _taskLogService = taskLogService;
            _taskTimeTrackingService = taskTimeTrackingService;
        }

        [HttpGet("GetTestProjectsAndTaskTypesByProjectTypeId")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<ProjectDropdownVM>>> GetTestProjectsAndTaskTypesByProjectTypeId(long projectTypeId)
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) projectReqsResponseResult;
            (ExecutionState executionState, List<TaskTypeVM> entity, string message) taskTypesResponseResult;
            (ExecutionState executionState, ProjectDropdownVM entity, string message) finalApiResponse;


            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                (ExecutionState executionState, List<ProjectRequestVM> entity, string message) projectResult = await _taskService.GetProjectReqsByProjectType(projectTypeId);
                (ExecutionState executionState, List<TaskTypeVM> entity, string message) taskTypeResult = await _taskService.GetTaskTypesByProjectType(projectTypeId);
                ProjectDropdownVM responseVM = new ProjectDropdownVM();
                if (projectResult.executionState == ExecutionState.Retrieved)
                {
                    projectReqsResponseResult.entity = projectResult.entity.Where(s => s.ProjectApprovalStatus == ProjectApprovalStatus.Accept).ToList();
                    projectReqsResponseResult.executionState = projectResult.executionState;
                    projectReqsResponseResult.message = projectResult.message;
                    responseVM.ProjectRequests = projectReqsResponseResult.entity.ToList();
                }
                if (taskTypeResult.executionState == ExecutionState.Retrieved)
                {
                    taskTypesResponseResult.entity = taskTypeResult.entity;
                    taskTypesResponseResult.executionState = taskTypeResult.executionState;
                    taskTypesResponseResult.message = taskTypeResult.message;
                    responseVM.TaskTypes = taskTypeResult.entity.ToList();
                    //return NotFound(apiResponse);
                }
                finalApiResponse.entity = responseVM;
                finalApiResponse.executionState = ExecutionState.Retrieved;
                finalApiResponse.message = "Data Found All";
                var apiResponse = new WebApiResponse<ProjectDropdownVM>(finalApiResponse);
                return Ok(apiResponse);

            }
            catch (Exception e)
            {
                finalApiResponse.entity = null;
                finalApiResponse.executionState = ExecutionState.Failure;
                finalApiResponse.message = e.Message;
                var apiResponse = new WebApiResponse<ProjectDropdownVM>(finalApiResponse);
                return StatusCode(500, apiResponse);
            }
        }

        [HttpGet("GetTasksByProjectId")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<List<TaskOfProjectVM>>>> GetTasksByProjectId(long projectId)
        {
            (ExecutionState executionState, List<TaskOfProjectVM> entity, string message) taskReqsResponseResult;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                (ExecutionState executionState, List<TaskOfProjectVM> entity, string message) taskResult = await _taskService.GetTaskByProjectId(projectId);
                List<TaskOfProjectVM> responseVM = new List<TaskOfProjectVM>();
                var apiResponse = new WebApiResponse<List<TaskOfProjectVM>>();
                if (taskResult.executionState == ExecutionState.Retrieved)
                {
                    taskReqsResponseResult.entity = taskResult.entity;
                    taskReqsResponseResult.executionState = taskResult.executionState;
                    taskReqsResponseResult.message = taskResult.message;
                    apiResponse = new WebApiResponse<List<TaskOfProjectVM>>(taskReqsResponseResult);
                }
                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                taskReqsResponseResult.entity = null;
                taskReqsResponseResult.executionState = ExecutionState.Failure;
                taskReqsResponseResult.message = e.Message;
                var apiResponse = new WebApiResponse<List<TaskOfProjectVM>>(taskReqsResponseResult);
                return StatusCode(500, apiResponse);
            }
        }

        public async override Task<ActionResult<WebApiResponse<TaskOfProjectVM>>> CreateAsync([FromBody] TaskOfProjectVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            //model.UserId = currentUserId;
            //model.UserType = UserType.Client_User;
            var result = await _taskService.CreateAsync(model);
            if (result.executionState == ExecutionState.Created)
            {
                TaskLogVM prLog = new TaskLogVM();
                prLog.TaskOfProjectId = result.entity.Id;
                //prLog.ProjectRequest = result.entity;
                prLog.Description = "Task created";
                prLog.CreatedBy = currentUserId;
                prLog.CreatedAt = result.entity.CreatedAt;
                var logResult = await _taskLogService.CreateAsync(prLog);
            }
            var apiResponse = new WebApiResponse<TaskOfProjectVM>(result);

            return Ok(apiResponse);

        }
        [HttpPost("CreateTimeTrackingOfATask")]
        [Authorize]
        public virtual async Task<ActionResult<WebApiResponse<TaskTimeTrackingVM>>> CreateTimeTrackingAsync([FromBody] TaskTimeTrackingVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            //model.UserId = currentUserId;
            //model.UserType = UserType.Client_User;
            var result = await _taskTimeTrackingService.CreateAsync(model);

            var apiResponse = new WebApiResponse<TaskTimeTrackingVM>(result);

            return Ok(apiResponse);

        }
        [HttpGet("GetTaskListByUserId/{userId}")]
        [Authorize]
        public virtual async Task<ActionResult<WebApiResponse<List<TaskOfProjectVM>>>> GetTaskListByUserId(long userId)
        {

            var result = await _taskService.GetTaskListByUserId(userId);
            var allTimeTrackings = await _taskTimeTrackingService.List();
            var filteredTrackings = allTimeTrackings.entity ?? new List<TaskTimeTrackingVM>();
            List<TaskOfProjectVM> customList = new List<TaskOfProjectVM>();

            foreach (var item in result.entity)
            {
                TaskOfProjectVM task = new TaskOfProjectVM();
                task.Id = item.Id;
                task.TaskId = item.TaskId;
                task.ProjectRequestId = item.ProjectRequestId;
                task.ProjectRequest = item.ProjectRequest;
                task.TaskTypeId = item.TaskTypeId;
                task.TaskPriority = item.TaskPriority;
                task.TaskType = item.TaskType;
                task.User = item.User;
                task.UserId = item.UserId;
                task.TaskEstimationHour = item.TaskEstimationHour;
                task.TotalTrackedTime = filteredTrackings.Where(s => s.TaskOfProjectId == item.Id)?.Sum(x => x.TimeSpentHour);
                task.TotalDueTime = (task.TaskEstimationHour - task.TotalTrackedTime);
                task.TaskEstimationHour = item.TaskEstimationHour;
                task.CreatedAt = item.CreatedAt;
                task.TaskDeadline = item.TaskDeadline;
                customList.Add(task);
            }

            result.entity = customList.OrderByDescending(s => s.Id).ToList();

            var apiResponse = new WebApiResponse<List<TaskOfProjectVM>>(result);

            return Ok(apiResponse);

        }


        public override async Task<ActionResult<WebApiResponse<IList<TaskOfProjectVM>>>> List()
        {

            var result = await _taskService.List();
            var allTimeTrackings = await _taskTimeTrackingService.List();
            allTimeTrackings.entity = allTimeTrackings.entity ?? new List<TaskTimeTrackingVM>();

            IList<TaskOfProjectVM> customList = new List<TaskOfProjectVM>();

            foreach (var item in result.entity ?? new List<TaskOfProjectVM>())
            {
                TaskOfProjectVM task = new TaskOfProjectVM();
                task.Id = item.Id;
                task.TaskId = item.TaskId;
                task.ProjectRequestId = item.ProjectRequestId;
                task.ProjectRequest = item.ProjectRequest;
                task.TaskTypeId = item.TaskTypeId;
                task.TaskPriority = item.TaskPriority;
                task.TaskType = item.TaskType;
                task.User = item.User;
                task.UserId = item.UserId;
                task.TaskEstimationHour = item.TaskEstimationHour;
                task.TotalTrackedTime = allTimeTrackings.entity.Where(s => s.TaskOfProjectId == item.Id)?.Sum(x => x.TimeSpentHour);
                task.TotalDueTime = (task.TaskEstimationHour - task.TotalTrackedTime);
                task.TaskEstimationHour = item.TaskEstimationHour;
                task.CreatedAt = item.CreatedAt;
                task.TaskDeadline = item.TaskDeadline;
                task.TaskTitle = item.TaskTitle;
                task.TaskOfProjectStatus = item.TaskOfProjectStatus;
                customList.Add(task);
            }

            result.entity = customList.OrderByDescending(s => s.Id).ToList();

            var apiResponse = new WebApiResponse<IList<TaskOfProjectVM>>(result);

            return Ok(apiResponse);
        }

        

        [HttpGet("GetTasksTimeTrackList")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<List<TaskTimeTrackingVM>>>> GetTasksTimeTrackList()
        {
            (ExecutionState executionState, List<TaskTimeTrackingVM> entity, string message) timeTrackResponseResult;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var taskResult = await _taskTimeTrackingService.List();
                var apiResponse = new WebApiResponse<List<TaskTimeTrackingVM>>();
                if (taskResult.executionState == ExecutionState.Retrieved)
                {
                    timeTrackResponseResult.entity = taskResult.entity.ToList();
                    timeTrackResponseResult.executionState = taskResult.executionState;
                    timeTrackResponseResult.message = taskResult.message;
                    apiResponse = new WebApiResponse<List<TaskTimeTrackingVM>>(timeTrackResponseResult);
                }
                else
                {
                    timeTrackResponseResult.entity = new List<TaskTimeTrackingVM>();
                    timeTrackResponseResult.executionState = taskResult.executionState;
                    timeTrackResponseResult.message = taskResult.message;
                    apiResponse = new WebApiResponse<List<TaskTimeTrackingVM>>(timeTrackResponseResult);

                }
                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                timeTrackResponseResult.entity = new List<TaskTimeTrackingVM>();
                timeTrackResponseResult.executionState = ExecutionState.Failure;
                timeTrackResponseResult.message = e.Message;
                var apiResponse = new WebApiResponse<List<TaskTimeTrackingVM>>(timeTrackResponseResult);
                return StatusCode(500, apiResponse);
            }
        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<TaskOfProjectVM>>>> Search(long? ProjectRequestId, string? TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline)
        {
            try
            {
                var (executionState, entity, message) = await _taskService.Search(ProjectRequestId, TaskId,AssigneeId,CreateDate,Deadline);

                return Ok(new WebApiResponse<IList<TaskOfProjectVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<TaskOfProjectVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }
    }
}

