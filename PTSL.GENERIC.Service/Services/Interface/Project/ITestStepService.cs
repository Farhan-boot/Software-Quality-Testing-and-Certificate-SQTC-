using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Project
{
    public interface ITestStepService : IBaseService<TestStepVM, TestStep>
    {
        Task<(ExecutionState executionState, TestStepVM entity, string message)> CreateListOfTestStep(List<TestStepVM> model);
        Task<(ExecutionState executionState, IList<TestStepVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId);
    }
}
