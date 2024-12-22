using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using ProjectRequestVM = PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project.ProjectRequestVM;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class PaymentCalculationHeaderVM : BaseModel
    {
        public long? ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long? TaskOfProjectId { get; set; }
        public TaskVM? TaskOfProject { get; set; }

        public Decimal? GrandTotal { get; set; }
        public string? GrandTotalInWord { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public Decimal? NetTotal { get; set; }

        public List<PaymentCalculationRowVM>? PaymentCalculationRows { get; set; }
        public List<PaymentInformationVM>? PaymentInformation { get; set; }

        public string? PaymentCalculationRowsJson { get; set; }

        //Add New
        public Decimal? Price { get; set; }
        public string? CreatedByName { get; set; }
    }
	
}
