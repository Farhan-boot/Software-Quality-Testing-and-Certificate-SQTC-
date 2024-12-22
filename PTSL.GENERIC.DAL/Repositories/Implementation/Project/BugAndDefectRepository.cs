using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class BugAndDefectRepository : BaseRepository<BugAndDefect>, IBugAndDefectRepository
    {
        private readonly GENERICReadOnlyCtx ReadOnlyCtx;
        public BugAndDefectRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            ReadOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, IList<BugAndDefect> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity)
        {
            IQueryable<BugAndDefect> query = ReadOnlyCtx.Set<BugAndDefect>()
                .Include(x => x.ProjectRequest)
                .Include(x => x.TestCase)
                .Include(x => x.TaskOfProject);

            
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(TaskOfProjectId is not null, x => x.TaskOfProjectId == TaskOfProjectId);
            query = query.WhereIf(TestCaseId is not null, x => x.TestCaseId == TestCaseId);
            query = query.WhereIf(!string.IsNullOrEmpty(bugzillaId), x => x.BugzillaId == bugzillaId);
            query = query.WhereIf(!string.IsNullOrEmpty(defectId), x => x.DefectId == defectId);
            query = query.WhereIf(bugAndDefectStatus is not null, x => x.BugAndDefectStatus == bugAndDefectStatus);
            query = query.WhereIf(bugAndDefectSeverity is not null, x => x.BugAndDefectSeverity == bugAndDefectSeverity);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }
    }
}
