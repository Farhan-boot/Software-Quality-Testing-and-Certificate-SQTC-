using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Project
{
    public interface IProjectRequestService
    {
        Task<(ExecutionState executionState, List<ProjectRequestVM> entity, string message)> List();
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectListByClientId(long clientId);
        (ExecutionState executionState, List<ProjectRequestLogVM> entity, string message) LogHistoryById(long id);
        Task<(ExecutionState executionState, ProjectRequestVM entity, string message)> Create(ProjectRequestVM model);
        Task<(ExecutionState executionState, ProjectRequestVM entity, string message)> GetById(long? id);
        Task<(ExecutionState executionState, ProjectRequestVM entity, string message)> Update(ProjectRequestVM model);
        Task<(ExecutionState executionState, ProjectRequestVM entity, string message)> Delete(long? id);
        Task<(ExecutionState executionState, string message)> DoesExist(long? id);
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode, long? ClientId, DateTime? RequestDate); 
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectPendingList();
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectRejectedList();
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectAcceptedList();

    }
}
