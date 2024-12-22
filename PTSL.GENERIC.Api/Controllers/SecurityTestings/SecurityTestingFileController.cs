using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.SecurityTestings;
using PTSL.GENERIC.Service.Services;

namespace PTSL.GENERIC.Api.Controllers.SecurityTestings
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityTestingFileController : BaseController<ISecurityTestingFileService, SecurityTestingFileVM, SecurityTestingFile>
    {
        private readonly ISecurityTestingFileService _SecurityTestingFileService;
        public SecurityTestingFileController(ISecurityTestingFileService SecurityTestingFileService) :
            base(SecurityTestingFileService)
        {
            _SecurityTestingFileService = SecurityTestingFileService;
        }

        //Implement here new api, if we want.


    }
}
