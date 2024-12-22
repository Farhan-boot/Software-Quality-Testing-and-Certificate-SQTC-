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
    public interface IProjectRequestService : IBaseService<ProjectRequestVM, ProjectRequest>
    {
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode, long? ClientId, DateTime? RequestDate);
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectPendingList();
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectRejectedList();
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectListByClientId(long clientId);
        Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectAcceptedList();

    }
}
