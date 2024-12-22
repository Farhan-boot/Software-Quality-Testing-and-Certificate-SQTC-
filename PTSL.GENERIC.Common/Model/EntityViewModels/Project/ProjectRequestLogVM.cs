using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class ProjectRequestLogVM : BaseModel
    {
        public long? ProjectRequestId { get; set; }
        public ProjectRequestVM ProjectRequest { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedUserName {  get; set; }
    }
}
