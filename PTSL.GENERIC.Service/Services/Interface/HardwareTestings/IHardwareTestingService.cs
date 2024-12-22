using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.HardwareTestings;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.HardwareTestings
{
    public interface IHardwareTestingService : IBaseService<HardwareTestingVM, HardwareTesting>
    {
        Task<(ExecutionState executionState, HardwareTestingVM entity, string message)> CreateListOfHardwareTesting(List<HardwareTestingVM> model);
        Task<(ExecutionState executionState, IList<HardwareTestingVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestScopeId, string? SubItem);
    }
}
