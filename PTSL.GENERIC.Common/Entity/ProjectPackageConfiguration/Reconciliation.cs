using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class Reconciliation : BaseEntity
    {
        public long? PaymentInformationId { get; set; } 
        public PaymentInformation? PaymentInformation { get; set; }
        public bool? IsPaymentDisbursement { get; set; }
        public DateTime? DisbursementDate { get; set; }
        public Decimal? DisbursementAmount { get; set; }
        public bool? IsPaymentRelease { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public Decimal? ReleaseAmount { get; set; }
        public string? ReconciliationRemark { get; set; }
        public bool? IsPaymentApproved { get; set; }
    }
}

