using System.ComponentModel.DataAnnotations;
namespace PTSL.GENERIC.Web.Core.Helper.Enum;
public enum ProcessFlowType

{
    [Display(Name = "Forward")]
    Forward = 1,
    [Display(Name = "backward")]
    backward = 2
}

