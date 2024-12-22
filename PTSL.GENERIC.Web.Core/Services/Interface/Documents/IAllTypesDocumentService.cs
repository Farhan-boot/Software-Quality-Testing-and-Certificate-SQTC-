using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Documents
{
    public interface IAllTypesDocumentService
    {
        Task<(ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message)> List();
        Task<(ExecutionState executionState, AllTypesOfDocumentVM entity, string message)> Create(AllTypesOfDocumentVM model);
        Task<(ExecutionState executionState, AllTypesOfDocumentVM entity, string message)> GetById(long? id);
        Task<(ExecutionState executionState, AllTypesOfDocumentVM entity, string message)> Update(AllTypesOfDocumentVM model);
        Task<(ExecutionState executionState, AllTypesOfDocumentVM entity, string message)> Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        Task<(ExecutionState executionState, IList<AllTypesOfDocumentVM> entity, string message)> ListByClientId(long clientId);

    }
}
