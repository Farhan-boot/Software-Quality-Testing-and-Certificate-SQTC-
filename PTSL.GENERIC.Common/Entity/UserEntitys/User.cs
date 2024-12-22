using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;

namespace PTSL.GENERIC.Common.Entity
{
    public class User : BaseEntity
    {
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RoleName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserPhone { get; set; }
        public string? UserGroup { get; set; }
        public int UserStatus { get; set; }

        public long? DesignationId { get; set; }
        public Designation? Designation { get; set; }


        public long? ClientId { get; set; }
        public Client? Client { get; set; }
        public long? UserRoleId { get; set; }
        public UserRole? UserRole { get; set; }

        public UserType UserType { get; set; }
        public List<PermissionHeaderSettings>? PermissionHeaderSettings { get; set; }
        public List<PermissionRowSettings>? PermissionRowSettings { get; set; }
        public List<TestCase>? TestCases { get; set; }
        public List<BugAndDefect>? BugAndDefects { get; set;}
        public List<AttendedUserMeeting>?AttendedUserMeeting { get; set; }
        public string? SignatureUrl { get; set; }
    }

    public class UserDropdownVM
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
    }
}
