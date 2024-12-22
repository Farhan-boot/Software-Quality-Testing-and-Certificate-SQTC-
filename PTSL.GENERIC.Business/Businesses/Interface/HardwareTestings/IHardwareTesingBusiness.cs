using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.HardwareTestings
{
    public interface IHardwareTestingBusiness : IBaseBusiness<HardwareTesting>
    {
        Task<(ExecutionState executionState, HardwareTesting entity, string message)> CreateListOfHardwareTesting(List<HardwareTesting> model);
        Task<(ExecutionState executionState, IList<HardwareTesting> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestScopeId, string? SubItem);
    }
}
