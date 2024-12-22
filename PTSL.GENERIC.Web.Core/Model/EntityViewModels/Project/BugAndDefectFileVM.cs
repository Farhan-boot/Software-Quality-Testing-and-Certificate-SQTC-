namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class BugAndDefectFileVM : BaseModel
    {
        public long BugAndDefectId { get; set; }
        public BugAndDefectVM? BugAndDefect { get; set; }

        public string? FileName { get; set; }
        public string? FileNameUrl { get; set; }
    }
}
