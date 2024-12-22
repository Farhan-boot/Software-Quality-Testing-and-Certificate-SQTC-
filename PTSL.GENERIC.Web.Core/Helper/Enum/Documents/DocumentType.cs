using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Web.Core.Enum.Documents
{
    public enum DocumentType
    {
        [Display(Name = "Agreement")]
        Agreement = 1,
        [Display(Name = "Payment")]
        Payment = 2,
        [Display(Name = "Functional Summary")]
        FunctionalSummary = 3,
        [Display(Name = "Functional Closure")]
        FunctionalClosure = 4,
        [Display(Name = "VAPT Summary")]
        VAPTSummary = 5,
        [Display(Name = "Load Testing")]
        LoadTesting = 6,
        [Display(Name = "Hardware Testing")]
        HardwareTesting = 7,
        [Display(Name = "Certification")] 
        Certification = 8,
        [Display(Name = "Money Receipt")]
        MoneyReceipt = 9
    }
}
