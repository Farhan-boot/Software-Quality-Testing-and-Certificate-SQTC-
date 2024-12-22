using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Project
{
    public interface IBugAndDefectBusiness : IBaseBusiness<BugAndDefect>
    {
        Task<(ExecutionState executionState, BugAndDefect entity, string message)> CreateListOfBugAndDefect(List<BugAndDefect> model);
        Task<(ExecutionState executionState, IList<BugAndDefect> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity);
        Task<(ExecutionState executionState, BugAndDefect entity, string message)> UpdateBugListOnBugZilla(List<BugAndDefect> entityList);
    }
}
