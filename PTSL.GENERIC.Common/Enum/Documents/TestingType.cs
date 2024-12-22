using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Enum.Documents
{
    public enum TestingType
    {

        [Display(Name = "Software Testing")]
        SoftwareTesting = 1,
        [Display(Name = "Hardware Testing")]
        HardwareTesting = 2,
        [Display(Name = "Security Testing")]
        SecurityTesting = 3
    }
}
