using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class BugAndDefectLog : BaseEntity
    {
        public long BugAndDefectId {  get; set; }
        public bool IsUpdateByBugzilla {  get; set; }
        public string LogRemarks {  get; set; } = string.Empty;
        public BugAndDefect? BugAndDefect { get; set; }
    }
}
