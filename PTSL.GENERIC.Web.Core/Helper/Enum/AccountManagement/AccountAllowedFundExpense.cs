using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.AccountManagement;

public enum AccountAllowedFundExpense
{
    [Display(Name = "LDF")]
    AIG = 1,

    [Display(Name = "Community Patrolling")]
    Patrolling = 2,

    CDF = 3,

    [Display(Name = "Internal Loan")]
    InternalLoan = 4,

    [Display(Name = "Office Rent")]
    OfficeRent = 5,

    [Display(Name = "Stationary")]
    Stationary = 6,

    [Display(Name = "Community Professional")]
    CommunityProfessional = 7,

    Others = 8,
}
