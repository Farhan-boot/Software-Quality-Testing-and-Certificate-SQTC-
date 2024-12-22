using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.SecurityTestings;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.DAL.UnitOfWork;

namespace PTSL.GENERIC.Business.Businesses.Implementation.SecurityTestings
{
    public class SecurityTestingFileBusiness : BaseBusiness<SecurityTestingFile>, ISecurityTestingFileBusiness
    {
        
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public SecurityTestingFileBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
        }


        
    }
}
