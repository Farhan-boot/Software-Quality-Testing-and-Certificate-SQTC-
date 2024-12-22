using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Project
{
    public interface IProjectStateLogRepository : IBaseRepository<ProjectStateLog>
    {
        Task<(ExecutionState executionState, ProjectStateLog entity,string message)>GetLogData(long projectRequestId,long projectStateEnumId);
    }
}
