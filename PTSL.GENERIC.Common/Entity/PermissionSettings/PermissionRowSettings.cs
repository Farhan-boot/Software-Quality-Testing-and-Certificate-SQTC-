using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.PermissionSettings
{
    public class PermissionRowSettings : BaseEntity
    {
        //Fk
        public long PermissionHeaderSettingsId { get; set; }
        public PermissionHeaderSettings? PermissionHeaderSettings { get; set; }

        //Other Info
        public long UserRoleId { get; set; }
        public UserRole? UserRole { get; set; }

        public long? UserId { get; set; }
        public User? User { get; set; }

        public long OrderId { get; set; }
    }
}
