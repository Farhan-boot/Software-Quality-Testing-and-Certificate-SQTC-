using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum
{
    public enum ProjectState
    {
        [Display(Name = "Project Request Submitted")]
        ProjectRequestSubmitted = 1,
        [Display(Name = "Initial Knowledge Sharing Meeting")]
        InitialKnowledgeSharingMeeting = 2,
        [Display(Name = "Project Accepted")]
        ProjectAccepted = 3,
        [Display(Name = "Project Rejected")]
        ProjectRejected = 4,
        [Display(Name = "Initial Documents Shared")]
        InitialDocumentsShared = 5,
        [Display(Name = "Agreement")]
        Agreement = 6,
        [Display(Name = "Payment Agreement")]
        PaymentAgreement = 7,
        [Display(Name = "Test Planning")]
        TestPlanning = 8,
        [Display(Name = "Testing In Progress")]
        TestingInProgress = 9,
        [Display(Name = "Initial Test Reporting")]
        InitialTestReporting = 10,
        [Display(Name = "Payment")]
        Payment = 11,
        [Display(Name = "Re-Testing In Progress")]
        ReTestingInProgress = 12,
        [Display(Name = "Final Closure Reporting")]
        FinalClosureReporting = 13,
        [Display(Name = "Certificate Generate & Download")]
        CertificateGenerateAndDownload = 14,
        [Display(Name = "Project Completed")]
        ProjectCompleted = 16
    }
}