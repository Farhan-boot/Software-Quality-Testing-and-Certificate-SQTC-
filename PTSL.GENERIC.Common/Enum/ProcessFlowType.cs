using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Common.Enum;

public enum ProcessFlowType

{
    [Display(Name = "Forward")]
    Forward = 1,
    [Display(Name = "Backward")]
    backward = 2
}

