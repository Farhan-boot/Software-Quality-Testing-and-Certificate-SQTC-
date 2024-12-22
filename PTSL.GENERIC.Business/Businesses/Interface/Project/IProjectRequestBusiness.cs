using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Project
{
    public interface IProjectRequestBusiness : IBaseBusiness<ProjectRequest>
    {
        Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode, long? ClientId, DateTime? RequestDate);
        Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> GetProjectPendingList();
        Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> GetProjectRejectedList();
        Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> GetProjectListByClientId(long clientId);
        Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> GetProjectAcceptedList();

    }
}
