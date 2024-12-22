using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.HardwareTestings;
using PTSL.GENERIC.Service.Services.Interface.HardwareTestings;

namespace PTSL.GENERIC.Api.Controllers.HardwareTestings
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HardwareTestingController : BaseController<IHardwareTestingService, HardwareTestingVM, HardwareTesting>
    {
        private readonly IHardwareTestingService _HardwareTestingService;
        public HardwareTestingController(IHardwareTestingService HardwareTestingService) :
            base(HardwareTestingService)
        {
            _HardwareTestingService = HardwareTestingService;
        }

        //Implement here new api, if we want.

        [HttpPost("CreateListOfHardwareTesting")]
        [Authorize]
        public async Task<ActionResult<WebApiResponse<HardwareTestingVM>>> CreateListOfHardwareTesting([FromBody] List<HardwareTestingVM> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            foreach (var item in model)
            {
                item.CreatedBy = currentUserId;
            }
            var result = await _HardwareTestingService.CreateListOfHardwareTesting(model);

            var apiResponse = new WebApiResponse<HardwareTestingVM>(result);

            return Ok(apiResponse);

        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<HardwareTestingVM>>>> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestScopeId, string? SubItem)
        {
            try
            {
                var (executionState, entity, message) = await _HardwareTestingService.Search(ProjectRequestId, TaskOfProjectId, TestScopeId,SubItem);

                return Ok(new WebApiResponse<IList<HardwareTestingVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<HardwareTestingVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
