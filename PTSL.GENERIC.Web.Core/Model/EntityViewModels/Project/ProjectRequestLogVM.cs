namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class ProjectRequestLogVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM ProjectRequest { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedUserName { get; set; }
    }
}
