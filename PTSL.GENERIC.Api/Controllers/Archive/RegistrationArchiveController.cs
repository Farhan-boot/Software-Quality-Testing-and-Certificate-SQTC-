using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Archive;
using PTSL.GENERIC.Service.Services.Archive;

namespace PTSL.GENERIC.Api.Controllers.Archive
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationArchiveController : BaseController<IRegistrationArchiveService, RegistrationArchiveVM, RegistrationArchive>
    {
        private readonly IRegistrationArchiveService _service;
        public RegistrationArchiveController(IRegistrationArchiveService service) :
            base(service)
        {
            _service = service;
        }



    }
}