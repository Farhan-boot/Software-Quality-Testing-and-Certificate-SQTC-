using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Project
{
    public interface ITestScenarioRepository : IBaseRepository<TestScenario>
    {
        Task<(ExecutionState executionState, List<TestScenario> entity, string message)> CreateListOfTestScenarioAsync(List<TestScenario> entityList);
        Task<(ExecutionState executionState, IList<TestScenario> entity, string message)> Search(long? ProjectRequestId, string? TestScenarioNo, TaskPriority? TaskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate);
    }
}
