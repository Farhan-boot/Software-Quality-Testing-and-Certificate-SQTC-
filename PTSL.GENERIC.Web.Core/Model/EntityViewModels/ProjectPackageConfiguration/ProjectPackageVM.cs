using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class ProjectPackageVM : BaseModel
    {
        public long? ProjectModuleNameId { get; set; }
        public ProjectModuleNameVM? ProjectModuleName { get; set; }
        public string? PackageName { get; set; }
        public string? PackageDescription { get; set; }
    }
	
}
