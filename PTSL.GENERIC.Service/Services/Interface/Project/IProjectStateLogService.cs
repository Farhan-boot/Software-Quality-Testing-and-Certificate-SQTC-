using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Project
{
    public interface IProjectStateLogService : IBaseService<ProjectStateLogVM, ProjectStateLog>
    {
        Task<(ExecutionState executionState, ProjectStateLogVM entity, string message)> GetLogData(long projectRequestId, long projectStateEnumId);
    }
}
