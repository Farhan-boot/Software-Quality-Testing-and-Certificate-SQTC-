using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.DAL.Repositories.Implementation.Project;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.UnitOfWork;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class AgreementDocumentBusiness : BaseBusiness<AgreementDocuments>, IAgreementDocumentBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public readonly IAgreementDocumentRepository _agreementDocumentRepository;
        public AgreementDocumentBusiness(GENERICUnitOfWork unitOfWork, IAgreementDocumentRepository agreementDocumentRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _agreementDocumentRepository = agreementDocumentRepository;
        }

    }
}
