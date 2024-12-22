using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Project
{
    public interface ITaskBusiness : IBaseBusiness<TaskOfProject>
    {
        Task<(ExecutionState executionState, List<ProjectRequest> entity, string message)> GetProjectReqsByProjectType(long projectTypeId);
        Task<(ExecutionState executionState, List<TaskType> entity, string message)> GetTaskTypesByProjectType(long projectTypeId);
        Task<(ExecutionState executionState, List<TaskOfProject> entity, string message)> GetTaskListByUserId(long userId);
        Task<(ExecutionState executionState, List<TaskOfProject> entity, string message)> GetTasksByProject(long projectId);
        Task<(ExecutionState executionState, IList<TaskOfProject> entity, string message)> Search(long? ProjectRequestId, string? TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline);

    }
}
