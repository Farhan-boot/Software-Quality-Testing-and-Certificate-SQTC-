using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestStepController : BaseController<ITestStepService, TestStepVM, TestStep>
    {
        private readonly ITestStepService _TestStepService;
        public TestStepController(ITestStepService TestStepService) :
            base(TestStepService)
        {
            _TestStepService = TestStepService;
        }

        //Implement here new api, if we want.

        [HttpPost("CreateListOfTestStep")]
        [Authorize]
        public async Task<ActionResult<WebApiResponse<TestStepVM>>> CreateListOfTestStep([FromBody] List<TestStepVM> model)
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
            var result = await _TestStepService.CreateListOfTestStep(model);

            var apiResponse = new WebApiResponse<TestStepVM>(result);

            return Ok(apiResponse);

        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<TestStepVM>>>> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId)
        {
            try
            {
                var (executionState, entity, message) = await _TestStepService.Search(ProjectRequestId, TaskOfProjectId, TestCaseId);

                return Ok(new WebApiResponse<IList<TestStepVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<TestStepVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
