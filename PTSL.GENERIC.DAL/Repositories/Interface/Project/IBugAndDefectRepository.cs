using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PTSL.GENERIC.Common.Enum;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Project
{
    public interface IBugAndDefectRepository : IBaseRepository<BugAndDefect>
    {
        Task<(ExecutionState executionState, IList<BugAndDefect> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity);
    }
}
