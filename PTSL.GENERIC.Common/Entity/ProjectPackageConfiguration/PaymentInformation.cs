using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Helper.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class PaymentInformation : BaseEntity
    {
        public long? PaymentCalculationHeaderId { get; set; }
        public PaymentCalculationHeader? PaymentCalculationHeader { get; set; }
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

        public List<DepositSlipFile>? DepositSlipFiles { get; set; }
        public long? DepositSlipFileId { get; set; }

        public List<Reconciliation>? Reconciliations { get; set; }

        public long? ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        
    }
}

