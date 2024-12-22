using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Project
{
    public interface IBugAndDefectService : IBaseService<BugAndDefectVM, BugAndDefect>
    {
        Task<(ExecutionState executionState, BugAndDefectVM entity, string message)> CreateListOfBugAndDefect(List<BugAndDefectVM> model);
        Task<(ExecutionState executionState, IList<BugAndDefectVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity);
        Task<(ExecutionState executionState, BugAndDefectVM entity, string message)> UpdateBugListOnBugzilla(List<BugAndDefectVM> model);
    }
}
