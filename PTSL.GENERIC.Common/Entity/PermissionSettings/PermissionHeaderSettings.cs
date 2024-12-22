using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Enum.PermissionSettings;

namespace PTSL.GENERIC.Common.Entity.PermissionSettings
{
    public class PermissionHeaderSettings : BaseEntity
    {
        public long? UserRoleId { get; set; }
        public UserRole? UserRole { get; set; }

        public long? UserId { get; set; }
        public User? User { get; set; }
        public ModuleEnum? ModuleEnumId { get; set; }

        // -- NOT NEEDED --

        public List<PermissionRowSettings>? PermissionRowSettings { get; set; }
    }
}
