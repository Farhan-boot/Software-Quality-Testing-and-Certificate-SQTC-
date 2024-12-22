using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.DAL.Repositories.Interface.SystemUser;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.SystemUser
{
    public class UserRolePermissionMapRepository : BaseRepository<UserRolePermissionMap>, IUserRolePermissionMapRepository
    {
        public UserRolePermissionMapRepository(
            GENERICWriteOnlyCtx writeOnlyCtx,
            GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx) { }
    }
}