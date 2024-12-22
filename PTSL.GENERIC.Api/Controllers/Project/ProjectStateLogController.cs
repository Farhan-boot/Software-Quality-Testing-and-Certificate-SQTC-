using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Implementation.Project;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectStateLogController : BaseController<IProjectStateLogService, ProjectStateLogVM, ProjectStateLog>
    {
        private readonly IProjectStateLogService _ProjectStateLogService;
        private readonly IProjectRequestLogService _ProjectRequestLogService;
        private readonly IUserService _UserService;
        public ProjectStateLogController(IProjectStateLogService ProjectStateLogService, IProjectRequestLogService ProjectRequestLogService, IUserService userService) :
            base(ProjectStateLogService)
        {
            _ProjectStateLogService = ProjectStateLogService;
            _ProjectRequestLogService = ProjectRequestLogService;
            _UserService = userService;
        }

        //Implement here new api, if we want.
        [HttpGet("GetLogData")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<ProjectStateLogVM>>> GetLogData(long projectRequestId, long projectStateEnumId)
        {
            try
            {
                var (executionState, entity, message) = await _ProjectStateLogService.GetLogData(projectRequestId, projectStateEnumId);

                return Ok(new WebApiResponse<ProjectStateLogVM>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<ProjectStateLogVM>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
