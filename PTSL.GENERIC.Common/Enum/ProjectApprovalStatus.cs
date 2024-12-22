using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Common.Enum;

public enum ProjectApprovalStatus
{
    [Display(Name = "Accepted")]
    Accept =1,
    [Display(Name = "Rejected")]
    Reject=2,
    [Display(Name = "Pending")]
    Pending = 3,
    [Display(Name = "In Pogress")]
    InPogress = 4,
    [Display(Name = "Completed")]
    Completed = 5,
}


