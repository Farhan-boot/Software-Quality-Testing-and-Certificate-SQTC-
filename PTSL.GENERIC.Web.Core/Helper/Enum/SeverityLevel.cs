using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum;

public enum SeverityLevel
{
    [Display(Name = "High")]
    High = 1,
    [Display(Name = "Medium")]
    Medium = 2,
    [Display(Name = "Low")]
    Low = 3,
}

