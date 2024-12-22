using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.HardwareTestings;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.HardwareTestings
{
    public interface ITestScopeService : IBaseService<TestScopeVM, TestScope>
    {
        Task<(ExecutionState executionState, TestScopeVM entity, string message)> CreateListOfTestScope(List<TestScopeVM> model);
        Task<(ExecutionState executionState, IList<TestScopeVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, string? TestItem,string? TenderID,string? SerialNo);
    }
}
