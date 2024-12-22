using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.DAL.Repositories.Interface
{
    public interface IClientLogRepository : IBaseRepository<ClientLog>
    {
        //Task<(ExecutionState executionState, User entity, string message)> UserLogin(LoginVM model);
    }
}
