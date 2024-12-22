using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public class ProjectPackageController : BaseController<IProjectPackageService, ProjectPackageVM, ProjectPackage>
    {
        private readonly IProjectPackageService _projectPackageService;
        public ProjectPackageController(IProjectPackageService service) :
            base(service)
        {
            _projectPackageService = service;
        }

 
        [HttpGet("GetProjectPackageByProjectModuleNameId/{ProjectModuleNameId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<ProjectPackageVM>>> GetProjectPackageByProjectModuleNameId([FromRoute] long ProjectModuleNameId)
        {
            (ExecutionState executionState, IList<ProjectPackageVM> entity, string message) result = await _projectPackageService.GetProjectPackageByProjectModuleNameId(ProjectModuleNameId);
            (ExecutionState executionState, IList<ProjectPackageVM> entity, string message) responseResult;

            if (result.executionState == ExecutionState.Retrieved)
            {
                responseResult.entity = result.entity.ToList();
                responseResult.message = result.message;
                responseResult.executionState = result.executionState;

                WebApiResponse<IList<ProjectPackageVM>> apiResponse = new WebApiResponse<IList<ProjectPackageVM>>(responseResult);
                return Ok(apiResponse);
            }
            else
            {
                responseResult.entity = null;
                responseResult.message = result.message;
                responseResult.executionState = result.executionState;

                WebApiResponse<IList<ProjectPackageVM>> apiResponse = new WebApiResponse<IList<ProjectPackageVM>>(responseResult);
                return NotFound(apiResponse);
            }
        }


    }
}