using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.SecurityTestings;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.SecurityTestings
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityTestingController : BaseController<ISecurityTestingService, SecurityTestingVM, SecurityTesting>
    {
        private readonly ISecurityTestingService _SecurityTestingService;
        public SecurityTestingController(ISecurityTestingService SecurityTestingService) :
            base(SecurityTestingService)
        {
            _SecurityTestingService = SecurityTestingService;
        }

        //Implement here new api, if we want.

        

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<SecurityTestingVM>>>> Search(long? ProjectRequestId, long? TaskOfProjectId, string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation)
        {
            try
            {
                var (executionState, entity, message) = await _SecurityTestingService.Search(ProjectRequestId, TaskOfProjectId,Vulnerability, SeverityLevel, EaseOfExploitation);

                return Ok(new WebApiResponse<IList<SecurityTestingVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<SecurityTestingVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
