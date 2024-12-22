using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<(ExecutionState executionState, IList<Client> entity, string message)> Search(string? organizationName, ClientStatus? ClientStatus,string? mobileNo, DateTime? CreatedAt);
        Task<(ExecutionState executionState, IList<User> entity,string message)> ClientWiseUserList(long ClientId);
       
    }
}
