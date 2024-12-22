using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class DepositSlipFile : BaseEntity
    {
        public long? PaymentInformationId { get; set; } 
        public PaymentInformation? PaymentInformation { get; set; }
        public string? FilePathUrl { get; set; }
    }
}

