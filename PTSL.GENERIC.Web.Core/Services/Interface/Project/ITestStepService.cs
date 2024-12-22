using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface ITestStepService
    {
        (ExecutionState executionState, List<TestStepVM> entity, string message) List();
        (ExecutionState executionState, TestStepVM entity, string message) Create(TestStepVM model);
        (ExecutionState executionState, TestStepVM entity, string message) GetById(long? id);
        (ExecutionState executionState, TestStepVM entity, string message) Update(TestStepVM model);
        (ExecutionState executionState, TestStepVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, TestStepVM entity, string message) CreateOfList(List<TestStepVM> model);
        Task<(ExecutionState executionState, IList<TestStepVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId);
    }
}
