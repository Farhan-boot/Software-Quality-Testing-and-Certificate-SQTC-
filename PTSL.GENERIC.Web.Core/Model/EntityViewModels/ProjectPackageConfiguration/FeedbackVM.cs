using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration
{
    public class FeedbackVM : BaseModel
    {
        public long? UserId { get; set; }
        public long? ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public bool? IsApprove { get; set; }
        public string? Comments { get; set; }
        public long? RatingCount { get; set; }
    }
	
}
