using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Model.GeneralSetup
{
    public class CertificationVM : BaseModel
    {
        [MaxLength(100)]
        public string CertificationName { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
    }

    
}
