using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;

namespace PTSL.GENERIC.Common.Model
{
    public class UserVM : BaseModel
    {
        [SwaggerExclude]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RoleName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserPhone { get; set; }
        public string? UserGroup { get; set; }
        public bool UserStatus { get; set; }
        public long PmsGroupID { get; set; }
        public long? GroupID { get; set; }
        public virtual UserGroupVM? Group { get; set; }
        public virtual PmsGroupVM? PmsGroup { get; set; }

        public long? UserRoleId { get; set; }
        [SwaggerExclude]
        public UserRoleVM? UserRole { get; set; }

        public UserType UserType { get; set; }
        public long? ClientId { get; set; }
        public ClientVM? Client { get; set; }
        public string? SignatureUrl { get; set; }

        public long? DesignationId { get; set; }
        public DesignationVM? Designation { get; set; }
    }

    public class LoginVM
    {
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginResultVM
    {
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }

        public string? Token { get; set; } // Need to be removed
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }

        public string? RoleName { get; set; }

        public long? UserRoleId { get; set; }
    }
}
