using System.Collections.Generic;
using System.Threading.Tasks;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Entity.HardwareTestings;

namespace PTSL.GENERIC.DAL.Repositories.Interface.HardwareTestings
{
    public interface IHardwareTestingRepository : IBaseRepository<HardwareTesting>
    {
        Task<(ExecutionState executionState, IList<HardwareTesting> entity, string message)> Search(long? ProjectRequestId,long? TaskOfProjectId,long? TestItemId, string? SubItem);
    }
}
