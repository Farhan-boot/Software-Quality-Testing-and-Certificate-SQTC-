using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings
{
    public class PermissionRowSettingsVM : BaseModel
    {
        //Fk
        public long PermissionHeaderSettingsId { get; set; }
        [SwaggerExclude]
        public PermissionHeaderSettings? PermissionHeaderSettings { get; set; }

        //Other Info
        public long UserRoleId { get; set; }
        [SwaggerExclude]
        public UserRole? UserRole { get; set; }
        public long? UserId { get; set; }
        [SwaggerExclude]
        public User? User { get; set; }
        public long OrderId { get; set; }
    }
}
