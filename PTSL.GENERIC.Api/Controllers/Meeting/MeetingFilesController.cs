using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Service.Services.Interface.Meetings;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingFilesController : BaseController<IMeetingFilesService, MeetingFilesVM, MeetingFiles>
    {
        public MeetingFilesController(IMeetingFilesService MeetingFileservice) :
            base(MeetingFileservice)
        { }

        //Implement here new api, if we want.
    }
}
