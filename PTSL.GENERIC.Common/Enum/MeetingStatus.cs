using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Common.Enum;

public enum MeetingStatus
{
    [Display(Name = "Completed")]
    Complete = 1,
    [Display(Name = "Incompleted")]
    Incomplete = 2,
    [Display(Name = "Accpted")]
    Accept = 3,
    [Display(Name = "Rejected")]
    Reject = 4,
    [Display(Name = "Canceled")]
    Cancel = 5,
    [Display(Name = "Pending")]
    Pending = 6
}

