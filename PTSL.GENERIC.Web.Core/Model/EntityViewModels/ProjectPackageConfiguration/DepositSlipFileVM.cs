using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class DepositSlipFileVM : BaseModel
    {
        public long? PaymentInformationId { get; set; }
        public PaymentInformationVM? PaymentInformation { get; set; }
        public string? FilePathUrl { get; set; }
    }
	
}
