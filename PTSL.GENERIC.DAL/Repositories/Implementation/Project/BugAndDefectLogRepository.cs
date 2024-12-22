using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class BugAndDefectLogRepository : BaseRepository<BugAndDefectLog>, IBugAndDefectLogRepository
    {
        private readonly GENERICReadOnlyCtx ReadOnlyCtx;
        public BugAndDefectLogRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            ReadOnlyCtx = readOnlyCtx;
        }
    }
}
