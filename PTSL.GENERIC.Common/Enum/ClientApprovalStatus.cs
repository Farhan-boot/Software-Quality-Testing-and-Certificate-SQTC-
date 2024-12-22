using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Common.Enum;

public enum ClientApprovalStatus
{
    [Display(Name = "Accepted")]
    Accept =1,
    [Display(Name = "Rejected")]
    Reject=2,
    [Display(Name = "Pending")]
    Pending = 3,
}


