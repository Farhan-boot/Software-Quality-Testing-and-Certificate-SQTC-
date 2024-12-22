using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.HardwareTestings
{
    public interface ITestScopeBusiness : IBaseBusiness<TestScope>
    {
        Task<(ExecutionState executionState, TestScope entity, string message)> CreateListOfTestScope(List<TestScope> model);
        Task<(ExecutionState executionState, IList<TestScope> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, string? TestItem, string? TenderID, string? SerialNo);
    }
}
