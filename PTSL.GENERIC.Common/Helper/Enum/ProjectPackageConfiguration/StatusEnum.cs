using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Common.Helper.ProjectPackageConfiguration
{
    public enum StatusEnum
    {
        [Display(Name = "Approved")]
        Approved = 1,
        [Display(Name = "Rejected")]
        Rejected = 2
    }
     
}
