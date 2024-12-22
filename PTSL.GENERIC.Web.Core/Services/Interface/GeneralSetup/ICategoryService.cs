using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup
{
    public interface ICategoryService
    {
        Task<(ExecutionState executionState, List<CategoryVM> entity, string message)> ListAsync();
        (ExecutionState executionState, CategoryVM entity, string message) Create(CategoryVM model);
        (ExecutionState executionState, CategoryVM entity, string message) GetById(long? id);
        (ExecutionState executionState, CategoryVM entity, string message) Update(CategoryVM model);
        (ExecutionState executionState, CategoryVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
