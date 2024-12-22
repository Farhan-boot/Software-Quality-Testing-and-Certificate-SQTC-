using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Project
{
    public interface ITaskService
    {
        Task<(ExecutionState executionState, List<TaskVM> entity, string message)> List();
        Task<(ExecutionState executionState, TaskVM entity, string message)> Create(TaskVM model);
        Task<(ExecutionState executionState, TaskVM entity, string message)> GetById(long? id);
        Task<(ExecutionState executionState, TaskVM entity, string message)> Update(TaskVM model);
        Task<(ExecutionState executionState, TaskVM entity, string message)> Delete(long? id);
        Task<(ExecutionState executionState, string message)> DoesExist(long? id);
        (ExecutionState executionState, TaskCascadingDDLVM entity, string message) GetProjectsAndTaskTypes(long projectTypeId);
        (ExecutionState executionState, TaskTimeTrackingVM entity, string message) CreateTimeTracking(TaskTimeTrackingVM model);
        Task<(ExecutionState executionState, List<TaskVM> entity, string message)> GetTaskListByUserId(long? userId);
        (ExecutionState executionState, List<TaskVM> entity, string message) GetTaskListByProjectId(long? id);
        (ExecutionState executionState, List<TaskTimeTrackingVM> entity, string message) GetTaskTimeTrackList();
        Task<(ExecutionState executionState, IList<TaskVM> entity, string message)> Search(long? ProjectRequestId, string? TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline);

    }
}
