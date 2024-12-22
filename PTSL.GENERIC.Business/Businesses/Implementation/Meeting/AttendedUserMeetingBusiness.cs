using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Meetings;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.DAL.Repositories.Interface.AttendedUserMeetings;
using PTSL.GENERIC.DAL.UnitOfWork;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Meetings
{
    public class AttendedUserMeetingBusiness : BaseBusiness<AttendedUserMeeting>, IAttendedUserMeetingBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public readonly IAttendedUserMeetingRepository _AttendedUserMeetingRepository;
        public AttendedUserMeetingBusiness(GENERICUnitOfWork unitOfWork, IAttendedUserMeetingRepository AttendedUserMeetingRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _AttendedUserMeetingRepository = AttendedUserMeetingRepository;
        }

    }
}
