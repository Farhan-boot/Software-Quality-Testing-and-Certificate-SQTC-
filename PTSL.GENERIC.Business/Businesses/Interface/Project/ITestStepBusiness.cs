using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Project
{
    public interface ITestStepBusiness : IBaseBusiness<TestStep>
    {
        Task<(ExecutionState executionState, TestStep entity, string message)> CreateListOfTestStep(List<TestStep> model);
        Task<(ExecutionState executionState, IList<TestStep> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId);
    }
}
