using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.Beneficiary;

public enum RecipientStatus
{
    Recipient = 1,

    [Display(Name = "Non-Recipient")]
    NonRecipient = 2,
}
