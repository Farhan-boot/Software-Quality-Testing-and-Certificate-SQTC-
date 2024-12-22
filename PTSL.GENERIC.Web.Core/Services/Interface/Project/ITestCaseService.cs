using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface ITestCaseService
    {
        (ExecutionState executionState, List<TestCaseVM> entity, string message) List();
        (ExecutionState executionState, TestCaseVM entity, string message) Create(TestCaseVM model);
        (ExecutionState executionState, TestCaseVM entity, string message) GetById(long? id);
        (ExecutionState executionState, TestCaseVM entity, string message) Update(TestCaseVM model);
        (ExecutionState executionState, TestCaseVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, TestCaseVM entity, string message) CreateOfList(List<TestCaseVM> model);
        Task<(ExecutionState executionState, List<TestCaseVM> entity, string message)> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate);
        (ExecutionState executionState, List<TestCaseVM> entity, string message) GetTestCasesByTaskofProjectId(long? id);
        Task<(ExecutionState executionState, List<TestCaseVM> entity, string message)> GetTestCaseListByProjectRequestId(long projectRequestId);

    }
}
