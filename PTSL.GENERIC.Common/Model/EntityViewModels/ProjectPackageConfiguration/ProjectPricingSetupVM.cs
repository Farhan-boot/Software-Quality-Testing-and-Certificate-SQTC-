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
    public class ProjectPricingSetupVM : BaseModel
    {
        public long? ProjectModuleNameId { get; set; }
        public ProjectModuleName? ProjectModuleName { get; set; }
        public long? ProjectPackageId { get; set; }
        public ProjectPackage? ProjectPackage { get; set; }
        public Decimal? Price { get; set; }
    }
}
