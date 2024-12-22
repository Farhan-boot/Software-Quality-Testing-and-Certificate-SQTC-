using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.DAL.Repositories.Implementation
{
    public class PmsGroupRepository : BaseRepository<PmsGroup>, IPmsGroupRepository
    {
        public PmsGroupRepository(
        GENERICWriteOnlyCtx GENERICWriteOnlyCtx,
        GENERICReadOnlyCtx dgFoodReadOnlyCtx)
        : base(GENERICWriteOnlyCtx, dgFoodReadOnlyCtx) { }
    }
}
