using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup
{
    public interface ICertificationService
    {
        (ExecutionState executionState, List<CertificationVM> entity, string message) List();
        (ExecutionState executionState, CertificationVM entity, string message) Create(CertificationVM model);
        (ExecutionState executionState, CertificationVM entity, string message) GetById(long? id);
        (ExecutionState executionState, CertificationVM entity, string message) Update(CertificationVM model);
        (ExecutionState executionState, CertificationVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
