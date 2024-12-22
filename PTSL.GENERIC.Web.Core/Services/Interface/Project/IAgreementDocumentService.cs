using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface IAgreementDocumentService
    {
        (ExecutionState executionState, List<AgreementDocumentsVM> entity, string message) List();
        (ExecutionState executionState, AgreementDocumentsVM entity, string message) Create(AgreementDocumentsVM model);
        (ExecutionState executionState, AgreementDocumentsVM entity, string message) GetById(long? id);
        (ExecutionState executionState, AgreementDocumentsVM entity, string message) Update(AgreementDocumentsVM model);
        (ExecutionState executionState, AgreementDocumentsVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
