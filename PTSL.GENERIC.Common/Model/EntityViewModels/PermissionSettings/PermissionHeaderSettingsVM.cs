using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum.PermissionSettings;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings
{
    public class PermissionHeaderSettingsVM : BaseModel
    {
        

        //Other Info
        public long? UserRoleId { get; set; }
        [SwaggerExclude]
        public UserRole? UserRole { get; set; }
        public long? UserId { get; set; }
        [SwaggerExclude]
        public User? User { get; set; }
        public ModuleEnum? ModuleEnumId { get; set; }
        [SwaggerExclude]
        public List<PermissionRowSettings>? PermissionRowSettings { get; set; }

        //New field Add
        public long? AccesslistId { get; set; }
        [SwaggerExclude]
        public Accesslist? Accesslist { get; set; }

    }
}
