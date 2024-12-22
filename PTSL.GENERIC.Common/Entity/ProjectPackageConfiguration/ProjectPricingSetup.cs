using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class ProjectPricingSetup : BaseEntity
    {
        public long? ProjectModuleNameId { get; set; } 
        public ProjectModuleName? ProjectModuleName { get; set; }
        public long? ProjectPackageId { get; set; }
        public ProjectPackage? ProjectPackage { get; set; }
        public Decimal? Price { get; set; }
    }
}

