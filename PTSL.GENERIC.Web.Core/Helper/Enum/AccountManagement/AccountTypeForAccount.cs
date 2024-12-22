using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.AccountManagement;

public enum AccountTypeForAccount
{
    [Display(Name = "Current Account")]
    CurrentAccount = 1,

    [Display(Name = "Savings Account")]
    SavingsAccount = 2,

    [Display(Name = "Fixed Deposit Account")]
    FixedDepositAccount = 3,

    [Display(Name = "Recurring Deposit Account")]
    RecurringDepositAccount = 4,
}
