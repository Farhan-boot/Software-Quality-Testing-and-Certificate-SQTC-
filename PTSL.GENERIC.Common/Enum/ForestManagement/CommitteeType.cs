using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Common.Enum.ForestManagement;

public enum CommitteeType
{
    [Display(Name = "Executive Committee")]
    ExecutiveCommittee = 1,
    [Display(Name = "Sub Executive Committee")]
    SubExecutiveCommittee = 2,
    [Display(Name = "Coordination Committee")]
    CoordinationCommittee = 3
}