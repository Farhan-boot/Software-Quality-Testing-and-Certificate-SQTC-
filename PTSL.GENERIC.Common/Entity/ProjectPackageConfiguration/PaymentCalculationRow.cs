using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class PaymentCalculationRow : BaseEntity
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

