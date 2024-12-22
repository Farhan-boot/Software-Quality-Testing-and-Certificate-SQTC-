using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model
{
    public class UserVM : BaseModel
	{
        public string? AccountsInformationsJson { get; set; }
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

        
		public long? ClientId { get; set; }
		public ClientVM? Client { get; set; }
        public long? UserRoleId { get; set; }
        public UserRoleVM? UserRole { get; set; }

        public UserType UserType { get; set; }
        public string? SignatureUrl { get; set; }

        public IFormFile? file { get; set; }
		public long? DesignationId { get; set; }
		public DesignationVM? Designation { get; set; }
    }

	public class LoginVM
	{
		public string? UserEmail { get; set; }
		public string? UserPassword { get; set; }
	}

	public class LoginResultVM
	{
		public long UserId { get; set; }
		public string? UserName { get; set; }
		public string? UserEmail { get; set; }
		public string? Token { get; set; }
		public string? RoleName { get; set; }

        public long? UserRoleId { get; set; }
    }
}
