using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Project
{
    public interface ITestScenarioService
    {
        Task<(ExecutionState executionState, List<TestScenarioVM> entity, string message)> List();
        Task<(ExecutionState executionState, TestScenarioVM entity, string message)> Create(TestScenarioVM model);
        (ExecutionState executionState, TestScenarioVM entity, string message) CreateOfList(List<TestScenarioVM> model);
        (ExecutionState executionState, TestScenarioVM entity, string message) GetById(long? id);
        Task<(ExecutionState executionState, TestScenarioVM entity, string message)> Update(TestScenarioVM model);
        Task<(ExecutionState executionState, TestScenarioVM entity, string message)> Delete(long? id);
        Task<(ExecutionState executionState, string message)> DoesExist(long? id);
        (ExecutionState executionState, List<TestScenarioVM> entity, string message) GetTestScenarioByTaskId(long? id);
        Task<(ExecutionState executionState, IList<TestScenarioVM> entity, string message)> Search(long? ProjectRequestId, string? TestScenarioNo, TaskPriority? TaskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate);
    }
}
