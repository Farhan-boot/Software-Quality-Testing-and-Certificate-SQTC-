using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.DAL.Repositories.Interface.PermissionSettings;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.PermissionSettings
{
    public class PermissionRowSettingsRepository : BaseRepository<PermissionRowSettings>, IPermissionRowSettingsRepository
    {
        public PermissionRowSettingsRepository(
            GENERICWriteOnlyCtx writeOnlyCtx,
            GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx) { }
    }
}