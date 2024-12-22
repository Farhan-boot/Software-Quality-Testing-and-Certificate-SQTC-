using PTSL.GENERIC.Common.Entity.Project;
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
    public class PaymentCalculationHeaderVM : BaseModel
    {
        public long? ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public long? TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }

        public Decimal? GrandTotal { get; set; }
        public string? GrandTotalInWord { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public Decimal? NetTotal { get; set; }

        public List<PaymentInformationVM>? PaymentInformation { get; set; }
        public List<PaymentCalculationRowVM>? PaymentCalculationRows { get; set; }

        public string? PaymentCalculationRowsJson { get; set; }

    }
}
