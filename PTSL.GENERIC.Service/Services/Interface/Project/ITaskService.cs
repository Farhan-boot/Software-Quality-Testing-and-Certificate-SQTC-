using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Project
{
    public interface ITaskService : IBaseService<TaskOfProjectVM, TaskOfProject>
    {
        Task<(ExecutionState executionState, List<ProjectRequestVM> entity, string message)> GetProjectReqsByProjectType(long projectTypeId);
        Task<(ExecutionState executionState, List<TaskTypeVM> entity, string message)> GetTaskTypesByProjectType(long projectTypeId);
        Task<(ExecutionState executionState, List<TaskOfProjectVM> entity, string message)> GetTaskListByUserId(long userId);
        Task<(ExecutionState executionState, List<TaskOfProjectVM> entity, string message)> GetTaskByProjectId(long projectId); 
        Task<(ExecutionState executionState, IList<TaskOfProjectVM> entity, string message)> Search(long? ProjectRequestId, string? TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline);

    }
}
