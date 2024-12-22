using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.Beneficiary;

public enum CIPHouseType
{
    [Display(Name = "Temporary")]
    Temporary = 1,

    [Display(Name = "Semi Temporary")]
    SemiTemporary = 2,

    [Display(Name = "Semi Permanent")]
    SemiPermanent = 3,

    [Display(Name = "Permanent")]
    Permanent = 4,
}
