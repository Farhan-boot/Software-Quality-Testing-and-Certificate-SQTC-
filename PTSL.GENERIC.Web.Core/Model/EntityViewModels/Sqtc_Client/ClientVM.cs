using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client
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
        //public IFormFile? OfficialLetterFile { get; set; }
        public long? UserId { get; set; }
        public UserVM? User { get; set; }
        public string? ApprovalMessage { get; set; }
        public bool IsApprovalShow { get; set; } 
        public bool IsAcceptOrReject { get; set; }
        public bool IsBackwardShow { get; set; }
    }
}
