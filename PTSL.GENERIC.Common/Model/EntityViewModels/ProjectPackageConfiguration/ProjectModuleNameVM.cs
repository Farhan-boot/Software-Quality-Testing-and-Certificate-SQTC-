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
    public class ProjectModuleNameVM : BaseModel
    {
        public string? Name { get; set; }
        public ProjectType? ProjectTypeId { get; set; }
        public List<ProjectPackageVM>? ProjectPackages { get; set; }

    }
}
