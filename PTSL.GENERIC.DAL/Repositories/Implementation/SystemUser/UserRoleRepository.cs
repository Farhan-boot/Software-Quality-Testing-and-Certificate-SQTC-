using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.DAL.Repositories.Interface;

namespace PTSL.GENERIC.DAL.Repositories.Implementation
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(
            GENERICWriteOnlyCtx gENERICWriteOnlyCtx,
            GENERICReadOnlyCtx gENERICReadOnlyCtx)
        : base(gENERICWriteOnlyCtx, gENERICReadOnlyCtx) { }
    }
}
