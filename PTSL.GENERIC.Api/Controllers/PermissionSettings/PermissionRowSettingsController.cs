using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.PermissionSettings;

namespace PTSL.GENERIC.Api.Controllers.PermissionSettings
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionRowSettingsController : BaseController<IPermissionRowSettingsService, PermissionRowSettingsVM, PermissionRowSettings>
    {
        private readonly IPermissionRowSettingsService _service;

        public PermissionRowSettingsController(IPermissionRowSettingsService service) :
            base(service)
        {
            _service = service;
        }

        [HttpGet("GetPermissionRowsByControllerName/{c}")]
        public async Task<ActionResult<WebApiResponse<List<PermissionRowSettingsVM>>>> GetPermissionRowsByControllerName(string c)
        {
            try
            {
                var (executionState, entity, message) = await _service.GetPermissionRowsByControllerName(c);

                return Ok(new WebApiResponse<List<PermissionRowSettingsVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch
            {
                return StatusCode(500, new WebApiResponse<List<PermissionRowSettingsVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }
    }
}