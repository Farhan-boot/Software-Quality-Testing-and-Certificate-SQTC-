using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.PermissionSettings
{
    public enum ModuleEnum
    {
        [Display(Name = "Registration Approval")]
        RegistrationApproval = 1,
        [Display(Name = "Project Request Approval")]
        TestProjectRequestApproval = 2,
        [Display(Name = "Agreement Approval")]
        AgreementApproval = 3,
        [Display(Name = "PaymentApproval")]
        PaymentApproval = 4,
        [Display(Name = "Report Approval")]
        ReportApproval = 5,
        [Display(Name = "Report Sending Approval")]
        ReportSendingApproval = 6
    }
}