using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client.ApprovalForProjectLogVM;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface IApprovalForProjectLogService
    {
        (ExecutionState executionState, List<ApprovalForProjectLogVM> entity, string message) List();
        (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) Create(ApprovalForProjectLogVM model);
        (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) Update(ApprovalForProjectLogVM model);
        (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        Task<(ExecutionState executionState, List<ApprovalForProjectLogVM> entity, string message)> ProjectCommentHistoryById(long id);
    }
}
