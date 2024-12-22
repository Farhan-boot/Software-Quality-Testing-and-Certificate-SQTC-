using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Documents
{
    public interface IDocumentsService
    {
        Task<(ExecutionState executionState, List<DocumentsByTypeVM> entity, string message)> List();
        (ExecutionState executionState, DocumentsByTypeVM entity, string message) Create(DocumentsByTypeVM model);
        (ExecutionState executionState, DocumentsByTypeVM entity, string message) GetById(long? id);
        (ExecutionState executionState, DocumentsByTypeVM entity, string message) Update(DocumentsByTypeVM model);
        (ExecutionState executionState, DocumentsByTypeVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, DocumentsByTypeVM entity, string message) CreateOfList(List<DocumentsByTypeVM> model);
        Task<(ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message)> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle);
        Task<(ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message)> DocumentsListByClientId(long clientId);

    }
}
