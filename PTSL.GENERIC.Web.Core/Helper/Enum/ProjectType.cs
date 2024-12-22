using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum
{
    public enum ProjectType
    {
        [Display(Name = "Software Testing")]
        SoftwareTesting = 1,
        [Display(Name = "Hardware Testing")]
        HardwareTesting = 2,
        [Display(Name = "Security Testing")]
        SecurityTesting = 3,
    }
}