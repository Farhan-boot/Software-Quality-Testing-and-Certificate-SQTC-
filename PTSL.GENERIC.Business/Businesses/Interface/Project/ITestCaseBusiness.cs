using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Project
{
    public interface ITestCaseBusiness : IBaseBusiness<TestCase>
    {
        Task<(ExecutionState executionState, IList<TestCase> entity, string message)> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate);
        Task<(ExecutionState executionState, TestCase entity, string message)> CreateListOfTestCase(List<TestCase> model);
        Task<(ExecutionState executionState,List<TestCase> entity, string message)> GetTestCasesByTaskofProjectId(long taskOfProjectId);
        Task<(ExecutionState executionState, IList<TestCase> entity, string message)> GetTestCaseListByProjectRequestId(long projectRequestId);
    }
}
