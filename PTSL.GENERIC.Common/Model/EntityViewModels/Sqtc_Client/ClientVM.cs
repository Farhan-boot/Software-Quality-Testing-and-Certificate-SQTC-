using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client
{
    public class ClientVM : BaseModel
    {
        public string ClientName { get; set; } = string.Empty;
        public string WorkingEmail { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string OrganizationEmail { get; set; } = string.Empty;
        public long DesignationId { get; set; }
        public DesignationVM? Designation { get; set; }
        public UserType? UserType { get; set; }
        public ClientStatus ClientStatus { get; set; }
        public ClientApprovalStatus ClientApprovalStatus { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? OfficialLetter { get; set; }
        public long? UserId { get; set; }
        public UserVM? User { get; set; }
    }
}
