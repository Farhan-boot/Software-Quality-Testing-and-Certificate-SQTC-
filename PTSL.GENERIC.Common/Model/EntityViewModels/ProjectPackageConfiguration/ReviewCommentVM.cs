using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Helper.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class ReviewCommentVM : BaseModel
    {

        public string? DocumentName { get; set; }
        public string? SectionName { get; set; }
        public string? ReviewComments { get; set; }
        public string? AuthorName { get; set; }
        public string? Solution { get; set; }
        public string? ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public StatusEnum? StatusEnumId { get; set; }

        public long? ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }

        public long? TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }

    }
}
