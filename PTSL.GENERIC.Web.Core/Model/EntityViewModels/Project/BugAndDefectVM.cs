using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.BugZilla;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class BugAndDefectVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskVM? TaskOfProject { get; set; }
        public long TestCaseId { get; set; }
        public TestCaseVM? TestCase { get; set; }
        public string DefectId { get; set; } = string.Empty;
        public string BugzillaId { get; set; } = string.Empty;
        public BugAndDefectSeverity BugAndDefectSeverity { get; set; }
        public BugAndDefectStatus BugAndDefectStatus { get; set; }
        public string? ExpectedResult { get; set; }
        public string? ActualResult { get; set; }
        public string? Resulation { get; set; }
        public string? DefectedSummary { get; set; }
        public string? StepstoReproduce { get; set; }
        public long ReportedBy { get; set; }
        public UserVM? User { get; set; }
        public DateTime? ReportedDate { get; set; }
        public List<BugAndDefectFileVM>? BugAndDefectFiles = new List<BugAndDefectFileVM>();
        public BugzillaBugStatus BugzillaBugStatus { get; set; }
        public string BugAndDefectStatusName { get; set; } = string.Empty;
        public string BugzillaBugStatusName { get; set; } = string.Empty;
    }
}
