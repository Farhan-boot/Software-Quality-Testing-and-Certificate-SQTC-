using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.SecurityTestings;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services
{
    public interface ISecurityTestingService : IBaseService<SecurityTestingVM, SecurityTesting>
    {
        Task<(ExecutionState executionState, IList<SecurityTestingVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation);
    }
}
