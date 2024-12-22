using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.Project
{
    public enum CertificationStatus
    {
        [Display(Name = "Generated With Draft")]
        GeneratedWithDraft = 1,
        [Display(Name = "Approved By the Client")]
        ApprovedByClient = 2,
        [Display(Name = "Amendment From Client")]
        AmendmentFromClient = 3,
        [Display(Name = "Approved By the Admin")]
        ApprovedByAdmin = 4
    }
}
