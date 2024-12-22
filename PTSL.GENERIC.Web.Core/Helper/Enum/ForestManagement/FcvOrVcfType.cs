using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.ForestManagement
{
    public enum FcvOrVcfType
    {
        [Display(Name = "FCV")]
        FCV = 0,
        [Display(Name = "VCF")]
        VCF = 1
    }
}