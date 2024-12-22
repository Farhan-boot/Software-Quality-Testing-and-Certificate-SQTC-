using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Model.EntityViewModels.Archive;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Archive;

namespace PTSL.GENERIC.Api.Controllers.Archive
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationArchiveFileController : BaseController<IRegistrationArchiveFileService, RegistrationArchiveFileVM, RegistrationArchiveFile>
    {
        public RegistrationArchiveFileController(IRegistrationArchiveFileService service) :
            base(service)
        {
        }
    }
}