using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class PaymentCalculationHeader : BaseEntity
    {
        public long? ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public long? TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }

        public Decimal? GrandTotal { get; set; }
        public string? GrandTotalInWord { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public Decimal? NetTotal { get; set; }

        public List<PaymentCalculationRow>? PaymentCalculationRows { get; set; }
        public List<PaymentInformation>? PaymentInformation { get; set; }

    }
}

