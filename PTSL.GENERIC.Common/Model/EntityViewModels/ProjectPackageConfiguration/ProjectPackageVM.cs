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
    public class ProjectPackageVM:BaseModel
    {
        public long? ProjectModuleNameId { get; set; }
        public ProjectModuleName? ProjectModuleName { get; set; }
        public string? PackageName { get; set; }
        public string? PackageDescription { get; set; }

    }
}
