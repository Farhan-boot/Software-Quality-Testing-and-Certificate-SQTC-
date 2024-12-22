using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.DAL.Repositories.Interface
{
    public interface ITaskTypeRepository : IBaseRepository<TaskType>
    {
        //Task<(ExecutionState executionState, User entity, string message)> UserLogin(LoginVM model);
    }
}
