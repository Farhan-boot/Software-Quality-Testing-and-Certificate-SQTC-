using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.DAL.Repositories.Implementation
{
    public class UserGroupRepository : BaseRepository<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(
        GENERICWriteOnlyCtx GENERICWriteOnlyCtx,
        GENERICReadOnlyCtx dgFoodReadOnlyCtx)
        : base(GENERICWriteOnlyCtx, dgFoodReadOnlyCtx) { }
    }
}
