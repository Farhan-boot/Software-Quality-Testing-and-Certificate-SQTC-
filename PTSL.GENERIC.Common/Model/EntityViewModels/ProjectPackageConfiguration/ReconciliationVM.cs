using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class ReconciliationVM : BaseModel
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
