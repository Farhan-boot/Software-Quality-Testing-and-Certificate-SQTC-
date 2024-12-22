using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Entity.SecurityTestings;

namespace PTSL.GENERIC.DAL.Repositories.Interface.SecurityTestings
{
    public interface ISecurityTestingRepository : IBaseRepository<SecurityTesting>
    {
        Task<(ExecutionState executionState, IList<SecurityTesting> entity, string message)> Search(long? ProjectRequestId,long? TaskOfProjectId,string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation);
    }
}
