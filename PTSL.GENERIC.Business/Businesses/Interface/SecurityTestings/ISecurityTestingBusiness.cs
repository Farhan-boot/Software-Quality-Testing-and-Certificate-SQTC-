using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.SecurityTestings
{
    public interface ISecurityTestingBusiness : IBaseBusiness<SecurityTesting>
    {
        
        Task<(ExecutionState executionState, IList<SecurityTesting> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation);
    }
}
