using Microsoft.AspNetCore.Http;
using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.Common.Entity.Sqtc_Client
{
    public class Client : BaseEntity
    {
        public string ClientName { get; set; } = string.Empty;
        public string WorkingEmail { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string OrganizationEmail { get; set; } = string.Empty;
        public long DesignationId { get; set; }
        public Designation? Designation { get; set; }
        public UserType? UserType { get; set; }
        public ClientStatus ClientStatus { get; set; }
        public ClientApprovalStatus ClientApprovalStatus { get; set; }  
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? OfficialLetter { get; set; }
        public long? UserId { get; set; }
        public  User? User { get; set; }
        public List<User>? ClientUsers { get; set; }   
    }
}

