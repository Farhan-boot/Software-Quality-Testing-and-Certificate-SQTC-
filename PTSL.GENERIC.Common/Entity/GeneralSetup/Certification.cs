using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.GeneralSetup
{
    public class Certification : BaseEntity
    {
        public string CertificationName { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
    }
}
