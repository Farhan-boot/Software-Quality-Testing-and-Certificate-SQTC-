using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.DAL.Repositories.Interface
{
    public interface IApprovalForRegisteredClientLogRepository : IBaseRepository<ApprovalForRegisteredClientLog>
    {
        //Task<(ExecutionState executionState, User entity, string message)> UserLogin(LoginVM model);
    }
}
