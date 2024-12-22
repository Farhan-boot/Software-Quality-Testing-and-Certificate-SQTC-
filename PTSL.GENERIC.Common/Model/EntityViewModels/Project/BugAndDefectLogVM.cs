using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class BugAndDefectLogVM : BaseModel
    {
        public long BugAndDefectId { get; set; }
        public bool IsUpdateByBugzilla { get; set; }
        public string LogRemarks { get; set; } = string.Empty;
        public BugAndDefectVM? BugAndDefect { get; set; }
    }
}
