using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class PaymentInformationVM : BaseModel
    {
        public long? PaymentCalculationHeaderId { get; set; }
        public PaymentCalculationHeaderVM? PaymentCalculationHeader { get; set; }
        public Decimal? PaymentAmount { get; set; }
        public PaymentMethodEnum? PaymentMethodEnumId { get; set; }
        //FromBankInfo
        public string? FromBankName { get; set; }
        public string? FromBranchName { get; set; }
        public string? FromAddress { get; set; }
        public string? FromAccountNumber { get; set; }
        public string? FromAccountName { get; set; }
        public DateTime? FromDepositDate { get; set; }
        public Decimal? FromDepositAmount { get; set; }
        //ToBankInfo
        public string? ToBankName { get; set; }
        public string? ToBranchName { get; set; }
        public string? ToAddress { get; set; }
        public string? ToAccountNumber { get; set; }
        public string? ToAccountName { get; set; }
        //ChequeInfo
        public string? ChequeNumber { get; set; }
        public string? ChequeDepositName { get; set; }
        public DateTime? ChequeDepositDate { get; set; }
        public string? ChequeDepositBankName { get; set; }
        public string? ChequeDepositBranchName { get; set; }
        public string? ChequeDepositAddress { get; set; }
        public string? ChequeDepositAccountNumber { get; set; }
        public string? ChequeDepositAccountName { get; set; }
        public Double? ChequeDepositAmount { get; set; }
        //BankDepositeInfo
        public string? BankDepositorName { get; set; }
        public DateTime? BankDepositeDate { get; set; }
        public string? BankDepositeBankName { get; set; }
        public string? BankDepositeBranchName { get; set; }
        public string? BankDepositeAddress { get; set; }
        public string? BankDepositeAccountNumber { get; set; }
        public string? BankDepositeAccountName { get; set; }
        public Double? BankDepositeDepositeAmount { get; set; }

        public List<DepositSlipFileVM>? DepositSlipFiles { get; set; } =new List<DepositSlipFileVM>();
        public long? DepositSlipFileId { get; set; }
        public List<ReconciliationVM>? Reconciliations { get; set; }
        public IReadOnlyList<IFormFile>? file { get; set; }

        public bool? IsPaymentApproved { get; set; }

        public long? ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }

    }
	
}
