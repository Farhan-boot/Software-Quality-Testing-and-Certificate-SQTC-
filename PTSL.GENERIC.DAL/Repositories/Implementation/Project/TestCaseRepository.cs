using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class TestCaseRepository : BaseRepository<TestCase>, ITestCaseRepository
    {
        public TestCaseRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            ReadOnlyCtx = readOnlyCtx;
        }
        public GENERICReadOnlyCtx ReadOnlyCtx { get; }

        public async Task<(ExecutionState executionState, IList<TestCase> entity, string message)> GetTestCaseListByProjectRequestId(long projectRequestId)
        {
            var query = ReadOnlyCtx.Set<TestCase>()
                .Where(x => x.ProjectRequestId == projectRequestId)
                .OrderByDescending(x=>x.Id)
                .ToListAsync();
            var result = await query.ConfigureAwait(false);
            return (ExecutionState.Retrieved, result, "Data Found Successfully");
        }

        public async Task<(ExecutionState executionState, IList<TestCase> entity, string message)> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate)
        {
            IQueryable<TestCase> query = ReadOnlyCtx.Set<TestCase>()
                .Include(x => x.ProjectRequest)
                .Include(x => x.TestScenario)
                .Include(x => x.TestCategory)
                .Include(x=>x.User);

            query = query.WhereIf(!string.IsNullOrEmpty(TestCaseNo), x => x.TestCaseNo == TestCaseNo);
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(TestScenarioId is not null, x => x.TestScenarioId == TestScenarioId);
            query = query.WhereIf(TestCategoryId is not null, x => x.TestCategoryId == TestCategoryId);
            query = query.WhereIf(ActualExecutionDate is not null, x => x.ActualExecutionDate == ActualExecutionDate);
            query = query.WhereIf(PlannedExecutionDate is not null, x => x.ActualExecutionDate == PlannedExecutionDate);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }
    }
}
