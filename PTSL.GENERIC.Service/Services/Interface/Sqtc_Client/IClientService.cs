using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model;

namespace PTSL.GENERIC.Service.Services
{
    public interface IClientService : IBaseService<ClientVM, Client>
    {
        Task<(ExecutionState executionState, IList<ClientVM> entity, string message)> Search(string? organizationName, ClientStatus? ClientStatus, string? mobileNo, DateTime? CreatedAt);
        Task<(ExecutionState executionState, IList<UserVM> entity, string message)> ClientWiseUserList(long ClientId);
    }
}
