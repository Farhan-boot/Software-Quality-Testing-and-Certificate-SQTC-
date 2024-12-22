using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class ProjectPackage : BaseEntity
    {
        public long? ProjectModuleNameId { get; set; } 
        public ProjectModuleName? ProjectModuleName { get; set; }
        public string? PackageName { get; set; }
        public string? PackageDescription { get; set; }
        public List<ProjectPricingSetup>? ProjectPricingSetup { get; set; }
    }
}

