using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class ProjectModuleNameVM : BaseModel
    {
        public string? Name { get; set; }
        public ProjectType? ProjectTypeId { get; set; }
        public List<ProjectPackageVM>? ProjectPackages { get; set; }
    }
	
}
