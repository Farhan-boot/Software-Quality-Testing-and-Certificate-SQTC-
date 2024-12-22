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
    public class TestScopeController : BaseController<ITestScopeService, TestScopeVM, TestScope>
    {
        private readonly ITestScopeService _TestScopeService;
        public TestScopeController(ITestScopeService TestScopeService) :
            base(TestScopeService)
        {
            _TestScopeService = TestScopeService;
        }

        //Implement here new api, if we want.

        [HttpPost("CreateListOfTestScope")]
        [Authorize]
        public async Task<ActionResult<WebApiResponse<TestScopeVM>>> CreateListOfTestScope([FromBody] List<TestScopeVM> model)
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
            var result = await _TestScopeService.CreateListOfTestScope(model);

            var apiResponse = new WebApiResponse<TestScopeVM>(result);

            return Ok(apiResponse);

        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<TestScopeVM>>>> Search(long? ProjectRequestId, long? TaskOfProjectId, string? TestItem, string? TenderID, string? SerialNo)
        {
            try
            {
                var (executionState, entity, message) = await _TestScopeService.Search(ProjectRequestId, TaskOfProjectId, TestItem,TenderID,SerialNo);

                return Ok(new WebApiResponse<IList<TestScopeVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<TestScopeVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
