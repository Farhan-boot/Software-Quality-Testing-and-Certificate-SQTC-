using System.Collections.Generic;
using System.Threading.Tasks;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Entity.HardwareTestings;

namespace PTSL.GENERIC.DAL.Repositories.Interface.HardwareTestings
{
    public interface ITestScopeRepository : IBaseRepository<TestScope>
    {
        Task<(ExecutionState executionState, IList<TestScope> entity, string message)> Search(long? ProjectRequestId,long? TaskOfProjectId,string? TestItem, string? TenderID,string? SerialNo);
    }
}
