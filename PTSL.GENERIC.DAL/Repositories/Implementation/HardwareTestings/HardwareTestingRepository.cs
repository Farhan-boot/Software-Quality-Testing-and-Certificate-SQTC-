using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.DAL.Repositories.Interface.HardwareTestings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.HardwareTestings
{
    public class HardwareTestingRepository : BaseRepository<HardwareTesting>, IHardwareTestingRepository
    {
        private readonly GENERICReadOnlyCtx ReadOnlyCtx;
        public HardwareTestingRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            ReadOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, IList<HardwareTesting> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,long? TestScopeId, string? SubItem)
        {
            IQueryable<HardwareTesting> query = ReadOnlyCtx.Set<HardwareTesting>()
                .Include(x => x.ProjectRequest)
                .Include(x => x.TaskOfProject)
                .Include(x=>x.TestScope);

            
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(TaskOfProjectId is not null, x => x.TaskOfProjectId == TaskOfProjectId);
            query = query.WhereIf(TestScopeId is not null, x => x.TestScopeId == TestScopeId);
            query = query.WhereIf(!string.IsNullOrEmpty(SubItem), x => x.SubItem == SubItem);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }

    }
}
