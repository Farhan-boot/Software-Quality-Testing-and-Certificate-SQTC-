using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Common.Enum;

public enum BugAndDefectSeverity
{
    [Display(Name = "Blocker")]
    Blocker = 1,
    [Display(Name = "Critical")]
    Critical = 2,
    [Display(Name = "Major")]
    Major = 3,
    [Display(Name = "Normal")]
    Normal = 4,
    [Display(Name = "Minor")]
    Minor = 5,
    [Display(Name = "Trivial")]
    Active = 6,
    [Display(Name = "Enhancement")]
    Enhancement = 7
}

