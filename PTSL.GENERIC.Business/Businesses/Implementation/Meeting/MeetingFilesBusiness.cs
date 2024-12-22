using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Meetings;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.DAL.Repositories.Interface.MeetingFiless;
using PTSL.GENERIC.DAL.UnitOfWork;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Meetings
{
    public class MeetingFilesBusiness : BaseBusiness<MeetingFiles>, IMeetingFilesBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public readonly IMeetingFilesRepository _MeetingFilesRepository;
        public MeetingFilesBusiness(GENERICUnitOfWork unitOfWork, IMeetingFilesRepository MeetingFilesRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _MeetingFilesRepository = MeetingFilesRepository;
        }

    }
}
