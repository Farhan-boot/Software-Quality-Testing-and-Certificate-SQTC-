using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Business.Businesses.Interface.Meetings;
using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Api.Controllers.ProjectPackageConfiguration
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectPricingSetupController : BaseController<IProjectPricingSetupService, ProjectPricingSetupVM, ProjectPricingSetup>
    {
        private readonly IProjectPricingSetupService _service;
        private readonly IProjectPricingSetupBusiness _business;
        public ProjectPricingSetupController(IProjectPricingSetupService service, IProjectPricingSetupBusiness business) :
            base(service)
        {
            _service = service;
            _business = business;
        }

        [HttpPost("GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<WebApiResponse<List<ProjectPricingSetupVM>>>> GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(long ProjectModuleNameId, long ProjectPackageId)
        {
            (ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message) responseResult;

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                (ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message) result = await _service.GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(ProjectModuleNameId, ProjectPackageId);
                if (result.executionState == ExecutionState.Retrieved)
                {
                    responseResult.entity = result.entity;
                    responseResult.executionState = result.executionState;
                    responseResult.message = result.message;
                    var apiResponse = new WebApiResponse<List<ProjectPricingSetupVM>>(responseResult);
                    return Ok(apiResponse);
                }
                else
                {
                    responseResult.entity = null;
                    responseResult.executionState = result.executionState;
                    responseResult.message = result.message;
                    var apiResponse = new WebApiResponse<List<ProjectPricingSetupVM>>(responseResult);
                    return NotFound(apiResponse);
                }
            }
            catch (Exception e)
            {
                responseResult.entity = null;
                responseResult.executionState = ExecutionState.Failure;
                responseResult.message = e.Message;
                var apiResponse = new WebApiResponse<List<ProjectPricingSetupVM>>(responseResult);
                return StatusCode(500, apiResponse);
            }
        }



    }
}