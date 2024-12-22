using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface ITestCategoryService
    {
        (ExecutionState executionState, List<TestCategoryVM> entity, string message) List();
        (ExecutionState executionState, TestCategoryVM entity, string message) Create(TestCategoryVM model);
        (ExecutionState executionState, TestCategoryVM entity, string message) GetById(long? id);
        (ExecutionState executionState, TestCategoryVM entity, string message) Update(TestCategoryVM model);
        (ExecutionState executionState, TestCategoryVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
