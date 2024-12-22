using PTSL.GENERIC.Web.Core.Helper.Enum.PermissionSettings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.PermissionSettings
{
    public class PermissionHeaderSettingsVM : BaseModel
    {
        

        //Other Info
        public long? UserRoleId { get; set; }
        public UserRoleVM? UserRole { get; set; }
        public long? UserId { get; set; }
        public UserVM? User { get; set; }
        public ModuleEnum? ModuleEnumId { get; set; }
        public List<PermissionRowSettingsVM>? PermissionRowSettings { get; set; }
        public string? PermissionRowSettingsJson { get; set; }



        //New field Add
        public long? AccesslistId { get; set; }
        public AccesslistVM? Accesslist { get; set; }
    }
}