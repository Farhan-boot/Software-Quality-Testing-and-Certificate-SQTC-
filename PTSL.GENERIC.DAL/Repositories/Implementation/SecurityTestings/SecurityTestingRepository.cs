using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.DAL.Repositories.Interface.SecurityTestings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.SecurityTestings
{
    public class SecurityTestingRepository : BaseRepository<SecurityTesting>, ISecurityTestingRepository
    {
        private readonly GENERICReadOnlyCtx ReadOnlyCtx;
        public SecurityTestingRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            ReadOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, IList<SecurityTesting> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,string? Vulnerability,SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation)
        {
            IQueryable<SecurityTesting> query = ReadOnlyCtx.Set<SecurityTesting>()
                .Include(x => x.ProjectRequest)
                .Include(x => x.TaskOfProject);

            
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(TaskOfProjectId is not null, x => x.TaskOfProjectId == TaskOfProjectId);
            query = query.WhereIf(SeverityLevel is not null, x => x.SeverityLevel == SeverityLevel);
            query = query.WhereIf(EaseOfExploitation is not null, x => x.EaseOfExploitation == EaseOfExploitation);
            query = query.WhereIf(!string.IsNullOrEmpty(Vulnerability), x => x.Vulnerability == Vulnerability);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }

    }
}
