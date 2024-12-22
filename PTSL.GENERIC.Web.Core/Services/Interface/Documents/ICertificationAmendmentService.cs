using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Documents
{
    public interface ICertificationAmendmentService
    {
        Task<(ExecutionState executionState, List<DocumentAmendmentVM> entity, string message)> List();
        Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> Create(DocumentAmendmentVM model);
        Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> GetById(long? id);
        Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> Update(DocumentAmendmentVM model);
        Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> CreateDocAmendment(DocumentAmendmentVM model);
        Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> DocumentAmendmentByDocId(long id);
    }
}
