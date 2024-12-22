using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Service.Services.Interface;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : BaseController<IModuleService, ModuleVM, Module>
    {
        public ModuleController(IModuleService service) : base(service)
        {
        }
    }
}
