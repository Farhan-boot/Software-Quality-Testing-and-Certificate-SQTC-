using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Service.Services.Interface.Meetings;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendedUserMeetingController : BaseController<IAttendedUserMeetingService, AttendedUserMeetingVM, AttendedUserMeeting>
    {
        public AttendedUserMeetingController(IAttendedUserMeetingService AttendedUserMeetingervice) :
            base(AttendedUserMeetingervice)
        { }

        //Implement here new api, if we want.
    }
}
