using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup
{
    public interface ITaskTypeService
    {
        Task<(ExecutionState executionState, List<TaskTypeVM> entity, string message)> List();
        (ExecutionState executionState, TaskTypeVM entity, string message) Create(TaskTypeVM model);
        (ExecutionState executionState, TaskTypeVM entity, string message) GetById(long? id);
        (ExecutionState executionState, TaskTypeVM entity, string message) Update(TaskTypeVM model);
        (ExecutionState executionState, TaskTypeVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
