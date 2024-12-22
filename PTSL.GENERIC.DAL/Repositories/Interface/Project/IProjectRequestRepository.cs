using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Project
{
    public interface IProjectRequestRepository : IBaseRepository<ProjectRequest>
    {
        Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode, long? ClientId, DateTime? RequestDate);
    }
}
