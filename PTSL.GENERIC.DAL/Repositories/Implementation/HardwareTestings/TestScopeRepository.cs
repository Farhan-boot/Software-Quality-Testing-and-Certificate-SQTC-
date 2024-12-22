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
    public class TestScopeRepository : BaseRepository<TestScope>, ITestScopeRepository
    {
        private readonly GENERICReadOnlyCtx ReadOnlyCtx;
        public TestScopeRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            ReadOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, IList<TestScope> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,string? TestItem,string? TenderID,string? SerialNo)
        {
            IQueryable<TestScope> query = ReadOnlyCtx.Set<TestScope>()
                .Include(x => x.ProjectRequest)
                .Include(x => x.TaskOfProject);

            
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(TaskOfProjectId is not null, x => x.TaskOfProjectId == TaskOfProjectId);
            query = query.WhereIf(!string.IsNullOrEmpty(TestItem), x => x.TestItem == TestItem);
            query = query.WhereIf(!string.IsNullOrEmpty(TenderID), x => x.TenderID == TenderID);
            query = query.WhereIf(!string.IsNullOrEmpty(SerialNo), x => x.SerialNo == SerialNo);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }

    }
}
