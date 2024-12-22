using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client
{
    public interface IClientService
    {
        Task<(ExecutionState executionState, List<ClientVM> entity, string message)> List();
        Task<(ExecutionState executionState, ClientVM entity, string message)> Create(ClientVM model);
        Task<(ExecutionState executionState, ClientVM entity, string message)> GetById(long? id);
        Task<(ExecutionState executionState, ClientVM entity, string message)> Update(ClientVM model);
        Task<(ExecutionState executionState, ClientVM entity, string message)> Delete(long? id);
        Task<(ExecutionState executionState, string message)> DoesExist(long? id);
        Task<(ExecutionState executionState, List<ClientVM> entity, string message)> Search(string? OrganizationName, ClientStatus? clientStatus, string? MobileNumber, DateTime? CreatedAt);
        Task<(ExecutionState executionState, ClientVM entity, string message)> ClientRegistration(ClientVM model);
        (ExecutionState executionState, List<ClientLogVM> entity, string message) ClientLogHistoryById(long id);
    }
}
