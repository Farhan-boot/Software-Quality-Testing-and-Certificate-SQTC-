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
    public class TestStepRepository : BaseRepository<TestStep>, ITestStepRepository
    {
        private readonly GENERICReadOnlyCtx ReadOnlyCtx;
        public TestStepRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            ReadOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, IList<TestStep> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId)
        {
            IQueryable<TestStep> query = ReadOnlyCtx.Set<TestStep>()
                .Include(x => x.ProjectRequest)
                .Include(x => x.TestCase)
                .Include(x => x.TaskOfProject);

            
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(TaskOfProjectId is not null, x => x.TaskOfProjectId == TaskOfProjectId);
            query = query.WhereIf(TestCaseId is not null, x => x.TestCaseId == TestCaseId);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }
    }
}
