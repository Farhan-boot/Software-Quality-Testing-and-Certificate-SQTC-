using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.DAL.Repositories.Interface;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class ProjectRequestRepository : BaseRepository<ProjectRequest>, IProjectRequestRepository
    {
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public ProjectRequestRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx) 
            : base(writeOnlyCtx, readOnlyCtx)
        {
            _readOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode, long? ClientId, DateTime? RequestDate)
        {
            IQueryable<ProjectRequest> query = _readOnlyCtx.Set<ProjectRequest>()
                .Include(x=>x.Client);

            query = query.WhereIf(!string.IsNullOrEmpty(ProjectName), x => x.ProjectName == ProjectName);
            query = query.WhereIf(!string.IsNullOrEmpty(ProjectCode), x => x.ProjectCode == ProjectCode);
            query = query.WhereIf(ProjectType is not null, x => x.ProjectType == ProjectType);
            query = query.WhereIf(ProjectCode is not null, x => x.ProjectCode == ProjectCode);
            query = query.WhereIf(ClientId is not null, x => x.ClientId == ClientId);
            query = query.WhereIf(RequestDate is not null, x => x.RequestDate == RequestDate);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }
    }
}
