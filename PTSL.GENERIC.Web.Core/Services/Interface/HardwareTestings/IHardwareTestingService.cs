using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.HardwareTestings;

namespace PTSL.eCommerce.Web.Core.Services.Interface.HardwareTestings
{
    public interface IHardwareTestingService
    {
        (ExecutionState executionState, List<HardwareTestingVM> entity, string message) List();
        (ExecutionState executionState, HardwareTestingVM entity, string message) Create(HardwareTestingVM model);
        (ExecutionState executionState, HardwareTestingVM entity, string message) GetById(long? id);
        (ExecutionState executionState, HardwareTestingVM entity, string message) Update(HardwareTestingVM model);
        (ExecutionState executionState, HardwareTestingVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, HardwareTestingVM entity, string message) CreateOfList(List<HardwareTestingVM> model);
        Task<(ExecutionState executionState, IList<HardwareTestingVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,long? TestScopeId,string? SubItem);
    }
}
