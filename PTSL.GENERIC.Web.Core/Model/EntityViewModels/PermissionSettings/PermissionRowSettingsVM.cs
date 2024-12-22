using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.PermissionSettings
{
    public class PermissionRowSettingsVM : BaseModel
    {
        //Fk
        public long PermissionHeaderSettingsId { get; set; }
        public PermissionHeaderSettingsVM PermissionHeaderSettings { get; set; }

        //Other Info
        public long UserRoleId { get; set; }

        public UserRoleVM? UserRole { get; set; }
        public long? UserId { get; set; }
        public UserVM? User { get; set; }

        public long OrderId { get; set; }
    }
}