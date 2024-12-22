using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface IApprovalForAllDocumentService
    {
        Task<(ExecutionState executionState, List<ApprovalForAllDocumentVM> entity, string message)> List();
        (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) Create(ApprovalForAllDocumentVM model);
        (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) Update(ApprovalForAllDocumentVM model);
        (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        Task<(ExecutionState executionState, List<ApprovalForAllDocumentVM> entity, string message)> DocumentCommentHistoryById(long id);
    }
}
