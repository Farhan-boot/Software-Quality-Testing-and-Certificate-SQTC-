using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class TaskLog : BaseEntity
    {
        public long TaskOfProjectId { get; set;}
        public TaskOfProject? TaskOfProject { get; set;}
        public string Description { get; set; } = string.Empty;
    }
}
