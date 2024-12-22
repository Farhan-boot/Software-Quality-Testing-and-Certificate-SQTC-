using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.DAL.UnitOfWork;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class ApprovalForRegisteredClientLogBusiness : BaseBusiness<ApprovalForRegisteredClientLog>, IApprovalForRegisteredClientLogBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public ApprovalForRegisteredClientLogBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
