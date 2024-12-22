using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum;

public enum TestResult
{
    [Display(Name = "Pass")]
    Pass = 1,
    [Display(Name = "Fail")]
    Fail = 2,
    [Display(Name = "Blocked")]
    Blocked = 3,
    [Display(Name = "Skipped")]
    Skipped = 4,
    [Display(Name = "Untested")]
    Untested = 5,
}

