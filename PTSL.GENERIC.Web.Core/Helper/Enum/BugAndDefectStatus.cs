using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum;

public enum BugAndDefectStatus
{
    [Display(Name = "Resolved")]
    Resolved = 1,
    [Display(Name = "Not Resolved")]
    NotResolved = 2,
}

