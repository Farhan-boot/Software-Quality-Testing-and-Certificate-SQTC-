using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Archive;
using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.DAL.UnitOfWork;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Archive
{
    public class RegistrationArchiveFileBusiness : BaseBusiness<RegistrationArchiveFile>, IRegistrationArchiveFileBusiness
    {
        public RegistrationArchiveFileBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}