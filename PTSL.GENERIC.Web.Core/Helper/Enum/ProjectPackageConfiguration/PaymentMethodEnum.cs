using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.ProjectPackageConfiguration
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
