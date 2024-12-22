using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Implementation.Project;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BugAndDefectFileController : BaseController<IBugAndDefectFileService, BugAndDefectFileVM, BugAndDefectFile>
    {
        private readonly IBugAndDefectFileService _BugAndDefectFileService;
        private readonly IProjectRequestLogService _ProjectRequestLogService;
        public BugAndDefectFileController(IBugAndDefectFileService BugAndDefectFileService, IProjectRequestLogService ProjectRequestLogService) :
            base(BugAndDefectFileService)
        {
            _BugAndDefectFileService = BugAndDefectFileService;
            _ProjectRequestLogService = ProjectRequestLogService;
        }

        //Implement here new api, if we want.
        
    }
}
