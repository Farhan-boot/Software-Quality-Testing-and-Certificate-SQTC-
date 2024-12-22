using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface IProjectStateLogService
    {
        (ExecutionState executionState, List<ProjectStateLogVM> entity, string message) List();
        (ExecutionState executionState, ProjectStateLogVM entity, string message) Create(ProjectStateLogVM model);
        (ExecutionState executionState, ProjectStateLogVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ProjectStateLogVM entity, string message) Update(ProjectStateLogVM model);
        (ExecutionState executionState, ProjectStateLogVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, ProjectStateLogVM entity, string message) GetLogData(long projectRequestId, long projectStateEnumId);
    }
}
