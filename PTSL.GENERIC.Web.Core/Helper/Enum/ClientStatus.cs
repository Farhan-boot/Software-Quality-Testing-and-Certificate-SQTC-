using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum;

public enum ClientStatus
{
    [Display(Name = "Active")]
    Active = 1,
    [Display(Name = "Inactive")]
    Inactive = 2,
}

