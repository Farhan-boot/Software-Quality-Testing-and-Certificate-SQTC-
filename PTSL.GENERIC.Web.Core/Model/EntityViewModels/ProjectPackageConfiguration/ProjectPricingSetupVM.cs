using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class ProjectPricingSetupVM : BaseModel
    {
        public long? ProjectModuleNameId { get; set; }
        public ProjectModuleNameVM? ProjectModuleName { get; set; }
        public long? ProjectPackageId { get; set; }
        public ProjectPackageVM? ProjectPackage { get; set; }
        public Decimal? Price { get; set; }

        public bool? IsExists { get; set; }
    }
	
}
