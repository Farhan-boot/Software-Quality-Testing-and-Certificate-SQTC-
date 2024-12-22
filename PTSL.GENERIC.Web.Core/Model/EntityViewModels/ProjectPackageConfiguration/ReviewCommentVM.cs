using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
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
        public ProjectRequestVM? ProjectRequest { get; set; }

        public long? TaskOfProjectId { get; set; }
        public TaskVM? TaskOfProject { get; set; }
    }
	
}
