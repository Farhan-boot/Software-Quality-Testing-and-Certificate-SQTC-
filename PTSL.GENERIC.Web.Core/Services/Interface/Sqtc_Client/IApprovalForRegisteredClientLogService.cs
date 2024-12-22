using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client.ApprovalForRegisteredClientLogVM;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client.ApprovalForRegisteredClientLog
{
    public interface IApprovalForRegisteredClientLogService
    {
        (ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message) List();
        Task<(ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message)> Create(ApprovalForRegisteredClientLogVM model);
        (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) Update(ApprovalForRegisteredClientLogVM model);
        (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        Task<(ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message)> ClientCommentHistoryById(long id);
        Task<(ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message)> CreateBackwardProcess(ApprovalForRegisteredClientLogVM model);
    }
}
