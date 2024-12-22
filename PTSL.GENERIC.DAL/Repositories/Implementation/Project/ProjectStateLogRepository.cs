using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class ProjectStateLogRepository : BaseRepository<ProjectStateLog>, IProjectStateLogRepository
    {
        private readonly GENERICReadOnlyCtx _gENERICReadOnlyCtx;
        public ProjectStateLogRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            _gENERICReadOnlyCtx= readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, ProjectStateLog entity, string message)> GetLogData(long projectRequestId, long projectStateEnumId)
        {
            var result =   _gENERICReadOnlyCtx.Set<ProjectStateLog>()
                .Where(x => x.ProjectRequestId == projectRequestId && ((long)x.ProjectState!) == projectStateEnumId)
                .FirstOrDefault();
            return (ExecutionState.Success,result ,"Data found");
        }
    }
}
