using PTSL.GENERIC.Common.Entity.Project;
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
    public class PaymentCalculationRowVM : BaseModel
    {
        public long? PaymentCalculationHeaderId { get; set; }
        public PaymentCalculationHeader? PaymentCalculationHeader { get; set; }
        public long? ProjectModuleNameId { get; set; }
        public ProjectModuleName? ProjectModuleName { get; set; }
        public long? ProjectPackageId { get; set; }
        public ProjectPackage? ProjectPackage { get; set; }
        public Decimal? UnitPrice { get; set; }
        public long? NumberOfPackage { get; set; }

        public Decimal? TotalPrice { get; set; }
        public Decimal? Vat { get; set; }
        public Decimal? Tax { get; set; }

    }
}
