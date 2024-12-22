using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class ReconciliationVM : BaseModel
    {
        public long? PaymentInformationId { get; set; }
        public PaymentInformationVM? PaymentInformation { get; set; }
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
