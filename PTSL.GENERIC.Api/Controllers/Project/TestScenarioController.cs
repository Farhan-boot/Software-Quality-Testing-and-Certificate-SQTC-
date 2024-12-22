using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.Services.Implementation.Project;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestScenarioController : BaseController<ITestScenarioService, TestScenarioVM, TestScenario>
    {
        private readonly ITestScenarioService _testScenarioService;
        private readonly ITaskLogService _taskLogService;
        private readonly ITaskTimeTrackingService _taskTimeTrackingService;
        public TestScenarioController(ITestScenarioService testScenarioService, ITaskLogService taskLogService, ITaskTimeTrackingService taskTimeTrackingService)
            : base(testScenarioService)
        {
            this._testScenarioService = testScenarioService;
            _taskLogService = taskLogService;
            _taskTimeTrackingService = taskTimeTrackingService;
        }

        [HttpPost("CreateListOfTestScenario")]
        [Authorize]
        public async Task<ActionResult<WebApiResponse<TestScenarioVM>>> CreateTestScenarioAsync([FromBody] List<TestScenarioVM> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            foreach (var item in model)
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedBy = currentUserId;
            }
            //model.UserId = currentUserId;
            //model.UserType = UserType.Client_User;
            var result = await _testScenarioService.CreateScenarioList(model);
            //if (result.executionState == ExecutionState.Created)
            //{
            //    TaskLogVM prLog = new TaskLogVM();
            //    prLog.TaskOfProjectId = result.entity.Id;
            //    //prLog.ProjectRequest = result.entity;
            //    prLog.Description = "Task created";
            //    prLog.CreatedBy = currentUserId;
            //    prLog.CreatedAt = result.entity.CreatedAt;
            //    var logResult = await _taskLogService.CreateAsync(prLog);
            //}
            var apiResponse = new WebApiResponse<TestScenarioVM>(result);

            return Ok(apiResponse);

        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override Task<ActionResult<WebApiResponse<TestScenarioVM>>> UpdateAsync([FromBody] TestScenarioVM model)
        {
            return base.UpdateAsync(model);
        }

        [HttpGet("GetTestScenarioByTaskId")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<List<TestScenarioVM>>>> GetTestScenarioByTaskId(long taskId)
        {
            (ExecutionState executionState, List<TestScenarioVM> entity, string message) taskReqsResponseResult;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                (ExecutionState executionState, List<TestScenarioVM> entity, string message) taskResult = await _testScenarioService.GetTestScenarioByTaskId(taskId);
                List<TestScenarioVM> responseVM = new List<TestScenarioVM>();
                var apiResponse = new WebApiResponse<List<TestScenarioVM>>();
                if (taskResult.executionState == ExecutionState.Retrieved)
                {
                    taskReqsResponseResult.entity = taskResult.entity;
                    taskReqsResponseResult.executionState = taskResult.executionState;
                    taskReqsResponseResult.message = taskResult.message;
                    apiResponse = new WebApiResponse<List<TestScenarioVM>>(taskReqsResponseResult);
                }
                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                taskReqsResponseResult.entity = null;
                taskReqsResponseResult.executionState = ExecutionState.Failure;
                taskReqsResponseResult.message = e.Message;
                var apiResponse = new WebApiResponse<List<TestScenarioVM>>(taskReqsResponseResult);
                return StatusCode(500, apiResponse);
            }
        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<TestScenarioVM>>>> Search(long? ProjectRequestId, string? TestScenarioNo, TaskPriority? TaskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate)
        {
            try
            {
                var (executionState, entity, message) = await _testScenarioService.Search(ProjectRequestId,TestScenarioNo,TaskPriority,CreatedBy,PlannedExecutionDate,ActualExecutionDate);

                return Ok(new WebApiResponse<IList<TestScenarioVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<TestScenarioVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }
    }
}
