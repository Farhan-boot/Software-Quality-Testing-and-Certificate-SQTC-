using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.HardwareTestings;

namespace PTSL.eCommerce.Web.Core.Services.Interface.HardwareTestings
{
    public interface ITestScopeService
    {
        (ExecutionState executionState, List<TestScopeVM> entity, string message) List();
        (ExecutionState executionState, TestScopeVM entity, string message) Create(TestScopeVM model);
        (ExecutionState executionState, TestScopeVM entity, string message) GetById(long? id);
        (ExecutionState executionState, TestScopeVM entity, string message) Update(TestScopeVM model);
        (ExecutionState executionState, TestScopeVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, TestScopeVM entity, string message) CreateOfList(List<TestScopeVM> model);
        Task<(ExecutionState executionState, IList<TestScopeVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,string? TestItem,string? TenderID, string? SerialNo);
    }
}
