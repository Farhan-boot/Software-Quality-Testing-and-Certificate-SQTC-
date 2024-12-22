using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Project
{
    public interface ITestScenarioService : IBaseService<TestScenarioVM, TestScenario>
    {
        Task<(ExecutionState executionState, TestScenarioVM entity, string message)> CreateScenarioList(List<TestScenarioVM> model);
        Task<(ExecutionState executionState, List<TestScenarioVM> entity, string message)> GetTestScenarioByTaskId(long taskId);
        Task<(ExecutionState executionState, IList<TestScenarioVM> entity, string message)> Search(long? ProjectRequestId, string TestScenarioNo, TaskPriority? TaskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate);
    }
}
