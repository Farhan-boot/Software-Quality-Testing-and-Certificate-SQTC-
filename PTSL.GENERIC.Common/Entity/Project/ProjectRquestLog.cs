using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class ProjectRquestLog : BaseEntity
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequest ProjectRequest { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
