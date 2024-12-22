using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectRequestController : BaseController<IProjectRequestService, ProjectRequestVM, ProjectRequest>
    {
        private readonly IProjectRequestService _projectRequestService;
        private readonly IProjectRequestLogService _projectRequestLogService;
        private readonly IUserService _userService;
        public ProjectRequestController(IProjectRequestService projectRequestService, IProjectRequestLogService projectRequestLogService, IUserService userService)
            : base(projectRequestService)
        {
            _projectRequestService = projectRequestService;
            _projectRequestLogService = projectRequestLogService;
            _userService = userService;

        }

        public async override Task<ActionResult<WebApiResponse<ProjectRequestVM>>> CreateAsync([FromBody] ProjectRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            model.UserId = currentUserId;
            //model.UserType = UserType.Client_User;
            var result = await _projectRequestService.CreateAsync(model);
            if (result.executionState == ExecutionState.Created)
            {
                ProjectRequestLogVM prLog = new ProjectRequestLogVM();
                prLog.ProjectRequestId = result.entity.Id;
                //prLog.ProjectRequest = result.entity;
                prLog.Description = "Project request created";
                prLog.CreatedBy = currentUserId;
                prLog.CreatedAt = result.entity.CreatedAt;
                var logResult = await _projectRequestLogService.CreateAsync(prLog);
            }
            var apiResponse = new WebApiResponse<ProjectRequestVM>(result);

            return Ok(apiResponse);

        }
        [HttpGet]
        [Route("GetProjectLogHistory/{projectId}")]
        public ActionResult<WebApiResponse<List<ProjectRequestLogVM>>> GetProjectLogHistory(long projectId)
        {
            (ExecutionState executionState, List<ProjectRequestLogVM> entity, string message) responseResult;
            try
            {
                var result = _projectRequestLogService.List().Result.entity.Where(s=>s.ProjectRequestId == projectId).ToList();
                var users = _userService.List().Result.entity.ToList();
                List<ProjectRequestLogVM> list = new List<ProjectRequestLogVM>();
                list = result.Select(s => new ProjectRequestLogVM()
                {
                    Id = s.Id,
                    Description = s.Description,
                    CreatedUserName = users.Where(x=>x.Id == s.CreatedBy).FirstOrDefault()?.UserName,
                    CreatedAt = s.CreatedAt,
                    ProjectRequestId = s.ProjectRequestId
                }).ToList();
                responseResult.entity = list;
                responseResult.executionState = ExecutionState.Retrieved;
                responseResult.message = "Log Found";
            }
            catch (Exception ex)
            {
                responseResult.entity = new List<ProjectRequestLogVM>();
                responseResult.executionState = ExecutionState.Failure;
                responseResult.message = "Error Occured";
            }
            WebApiResponse<List<ProjectRequestLogVM>> apiResponse = new WebApiResponse<List<ProjectRequestLogVM>>(responseResult);
            return Ok(apiResponse);
        }


        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<ProjectRequestVM>>>> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode,long? ClientId, DateTime? RequestDate)
        {
            try
            {
                var (executionState, entity, message) = await _projectRequestService.Search(ProjectName, ProjectType, ProjectCode, ClientId, RequestDate);

                return Ok(new WebApiResponse<IList<ProjectRequestVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<ProjectRequestVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("GetProjectPendingList")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<ProjectRequestVM>>>> GetProjectPendingList()
        {
            try
            {
                var (executionState, entity, message) = await _projectRequestService.GetProjectPendingList();

                return Ok(new WebApiResponse<IList<ProjectRequestVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<ProjectRequestVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("GetProjectRejectedList")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<ProjectRequestVM>>>> GetProjectRejectedList()
        {
            try
            {
                var (executionState, entity, message) = await _projectRequestService.GetProjectRejectedList();

                return Ok(new WebApiResponse<IList<ProjectRequestVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<ProjectRequestVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("GetProjectListByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<ProjectRequestVM>>>> GetProjectListByClientId(long clientId)
        {
            try
            {
                var (executionState, entity, message) = await _projectRequestService.GetProjectListByClientId(clientId);

                return Ok(new WebApiResponse<IList<ProjectRequestVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<ProjectRequestVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("GetProjectAcceptedList")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<ProjectRequestVM>>>> GetProjectAcceptedList()
        {
            try
            {
                var (executionState, entity, message) = await _projectRequestService.GetProjectAcceptedList();

                return Ok(new WebApiResponse<IList<ProjectRequestVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<ProjectRequestVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
