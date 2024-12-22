using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<(ExecutionState executionState,IList<User> entity, string message)> Search(long? userRoleId, string? userName, string? firstName, string? email, string? userPhone);
    }
}
