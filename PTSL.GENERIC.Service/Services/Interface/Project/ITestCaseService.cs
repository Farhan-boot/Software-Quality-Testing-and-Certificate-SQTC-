using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Project
{
    public interface ITestCaseService : IBaseService<TestCaseVM, TestCase>
    {
        Task<(ExecutionState executionState, TestCaseVM entity, string message)> CreateListOfTestCase(List<TestCaseVM> model);
        Task<(ExecutionState executionState, IList<TestCaseVM> entity, string message)> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate);
        Task<(ExecutionState executionState ,IList<TestCaseVM> entity , string message)> GetTestCasesByTaskofProjectId(long taskOfProjectId);
        Task<(ExecutionState executionState, IList<TestCaseVM> entity, string message)> GetTestCaseListByProjectRequestId(long projectRequestId);
    }
}
