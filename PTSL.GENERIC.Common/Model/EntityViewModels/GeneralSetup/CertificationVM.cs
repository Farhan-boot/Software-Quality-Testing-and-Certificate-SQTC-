using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup
{
    public class CertificationVM : BaseModel
    {
        public string CertificationName { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
    }
}
