using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Api.Helpers.ProjectPackageConfiguration
{
    public enum PaymentMethodEnum
    {
        [Display(Name = "BankTransfer")]
        BankTransfer = 1,
        [Display(Name = "Cheque Payment")]
        ChequePayment = 2,
        [Display(Name = "Bank Deposit")]
        BankDeposit = 3
    }
}
