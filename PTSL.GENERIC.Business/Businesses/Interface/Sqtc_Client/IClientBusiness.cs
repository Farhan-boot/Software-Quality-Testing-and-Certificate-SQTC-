using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Entity;

namespace PTSL.GENERIC.Business.Businesses.Interface
{
    public interface IClientBusiness : IBaseBusiness<Client>
    {
        Task<(ExecutionState executionState, IList<Client> entity, string message)> Search(string? organizationName, ClientStatus? ClientStatus, string? mobileNo, DateTime? CreatedAt);
        Task<(ExecutionState executionState, IList<User> entity, string message)> ClientWiseUserList(long ClientId);
    }
}
