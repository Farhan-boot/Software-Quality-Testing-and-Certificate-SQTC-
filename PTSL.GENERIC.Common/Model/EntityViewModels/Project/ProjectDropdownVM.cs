using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class ProjectDropdownVM
    {
        public ProjectDropdownVM()
        {
            ProjectRequests = new List<ProjectRequestVM>();
            TaskTypes = new List<TaskTypeVM>();
        }
        public List<ProjectRequestVM> ProjectRequests { get; set; }
        public List<TaskTypeVM> TaskTypes { get; set; }
    }
}
