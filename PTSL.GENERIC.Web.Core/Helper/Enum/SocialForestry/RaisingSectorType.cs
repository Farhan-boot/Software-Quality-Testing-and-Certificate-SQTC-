using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Helper.Enum.SocialForestry
{
    public enum RaisingSectorType
    {
        [Display(Name = "Distribution")]
        ForDistribution = 1,
        [Display(Name = "Distribution & Plantation")]
        ForDistributionAndPlantation = 2,
        [Display(Name = "Plantation")]
        ForPlantation = 3,
    }

}
