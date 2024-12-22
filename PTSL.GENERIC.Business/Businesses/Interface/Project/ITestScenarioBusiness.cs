using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Project
{
    public interface ITestScenarioBusiness : IBaseBusiness<TestScenario>
    {
        Task<(ExecutionState executionState, TestScenario entity, string message)> CreateScenarioListAsync(List<TestScenario> entityList);
        Task<(ExecutionState executionState, List<TestScenario> entity, string message)> GetTestScenarioByTaskId(long taskId);
        Task<(ExecutionState executionState, IList<TestScenario> entity, string message)> Search(long? ProjectRequestId, string TestScenarioNo, TaskPriority? TaskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate);
    }
}
