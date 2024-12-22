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
    public class TestCaseController : BaseController<ITestCaseService, TestCaseVM, TestCase>
    {
        private readonly ITestCaseService _TestCaseService;
        public TestCaseController(ITestCaseService TestCaseService) :
            base(TestCaseService)
        {
            _TestCaseService = TestCaseService;
        }

        [HttpPost("CreateListOfTestCase")]
        [Authorize]
        public async Task<ActionResult<WebApiResponse<TestCaseVM>>> CreateListOfTestCase([FromBody] List<TestCaseVM> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            foreach (var testCase in model)
            {
                testCase.CreatedBy = currentUserId;
            }
            var result = await _TestCaseService.CreateListOfTestCase(model);

            var apiResponse = new WebApiResponse<TestCaseVM>(result);

            return Ok(apiResponse);

        }

        //Implement here new api, if we want.


        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<TestCaseVM>>>> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate)
        {
            try
            {
                var (executionState, entity, message) = await _TestCaseService.Search(TestCaseNo, ProjectRequestId, TestScenarioId, TestCategoryId, ActualExecutionDate, PlannedExecutionDate);

                return Ok(new WebApiResponse<IList<TestCaseVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<TestCaseVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("GetTestCasesByTaskofProjectId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<TestCaseVM>>>> GetTestCasesByTaskofProjectId(long taskOfProjectId)
        {
            try
            {
                var (executionState, entity, message) = await _TestCaseService.GetTestCasesByTaskofProjectId(taskOfProjectId);

                return Ok(new WebApiResponse<IList<TestCaseVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<TestCaseVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("GetTestCaseListByProjectRequestId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<TestCaseVM>>>> GetTestCaseListByProjectRequestId(long projectRequestId)
        {
            try
            {
                var (executionState, entity, message) = await _TestCaseService.GetTestCaseListByProjectRequestId(projectRequestId);

                return Ok(new WebApiResponse<IList<TestCaseVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<TestCaseVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
