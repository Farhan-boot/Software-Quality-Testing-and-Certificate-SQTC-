using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class FeedbackVM : BaseModel
    {
        public long? UserId { get; set; }
        public long? ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public bool? IsApprove { get; set; }
        public string? Comments { get; set; }
        public long? RatingCount { get; set; }

    }
}
