using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class PaymentCalculationRowVM : BaseModel
    {
        public long? PaymentCalculationHeaderId { get; set; }
        public PaymentCalculationHeaderVM? PaymentCalculationHeader { get; set; }
        public long? ProjectModuleNameId { get; set; }
        public ProjectModuleNameVM? ProjectModuleName { get; set; }
        public long? ProjectPackageId { get; set; }
        public ProjectPackageVM? ProjectPackage { get; set; }
        public Decimal? UnitPrice { get; set; }
        public long? NumberOfPackage { get; set; }

        public Decimal? TotalPrice { get; set; }
        public Decimal? Vat { get; set; }
        public Decimal? Tax { get; set; }
    }
	
}
