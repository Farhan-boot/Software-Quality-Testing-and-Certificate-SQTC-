using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PTSL.GENERIC.Common.Enum;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Project
{
    public interface ITestStepRepository : IBaseRepository<TestStep>
    {
        Task<(ExecutionState executionState, IList<TestStep> entity, string message)> Search(long? ProjectRequestId,long? TaskOfProjectId,long? TestCaseId);
    }
}
