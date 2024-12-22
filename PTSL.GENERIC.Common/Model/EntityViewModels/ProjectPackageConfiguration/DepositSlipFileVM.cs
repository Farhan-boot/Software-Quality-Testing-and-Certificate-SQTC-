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
    public class DepositSlipFileVM : BaseModel
    {
        public long? PaymentInformationId { get; set; }
        public PaymentInformationVM? PaymentInformation { get; set; }
        public string? FilePathUrl { get; set; }
    }
}
