using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Web.Core.Helper.Enum
{
    public enum DocumentApprovalStatus
    {
        [Display(Name = "Accepted")]
        Accept = 1,
        [Display(Name = "Rejected")]
        Reject = 2,
        [Display(Name = "Pending")]
        Pending = 3,
        [Display(Name = "In Pogress")]
        InPogress = 4
    }
}
