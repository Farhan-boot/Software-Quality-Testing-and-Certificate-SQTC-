using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class ProjectModuleName : BaseEntity
    {
        public string? Name { get; set; } 
        public ProjectType? ProjectTypeId { get; set; }
        public List<ProjectPackage>? ProjectPackages { get; set; }
        public List<ProjectPricingSetup>? ProjectPricingSetup { get; set; }
    }
}

