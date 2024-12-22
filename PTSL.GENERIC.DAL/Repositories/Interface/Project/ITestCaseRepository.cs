using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Project
{
    public interface ITestCaseRepository : IBaseRepository<TestCase>
    {
        Task<(ExecutionState executionState, IList<TestCase> entity, string message)> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate);
        Task<(ExecutionState executionState, IList<TestCase> entity, string message)> GetTestCaseListByProjectRequestId(long projectRequestId);
    }
}
