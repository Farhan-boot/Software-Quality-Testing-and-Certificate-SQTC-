using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.DAL.Repositories.Interface.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class ProjectCertificationRepository : BaseRepository<ProjectCertification>, IProjectCertificationRepository
    {
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public ProjectCertificationRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            _readOnlyCtx = readOnlyCtx;
        }
    }
}
