using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SecurityTestings;

namespace PTSL.eCommerce.Web.Core.Services.Interface.SecurityTestings
{
    public interface ISecurityTestingService
    {
        (ExecutionState executionState, List<SecurityTestingVM> entity, string message) List();
        (ExecutionState executionState, SecurityTestingVM entity, string message) Create(SecurityTestingVM model);
        (ExecutionState executionState, SecurityTestingVM entity, string message) GetById(long? id);
        (ExecutionState executionState, SecurityTestingVM entity, string message) Update(SecurityTestingVM model);
        (ExecutionState executionState, SecurityTestingVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        Task<(ExecutionState executionState, IList<SecurityTestingVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation);
    }
}
