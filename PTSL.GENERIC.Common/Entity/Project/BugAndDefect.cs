using Humanizer;
using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.IO.Compression;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class BugAndDefect : BaseEntity
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }
        public long TestCaseId { get; set; }
        public TestCase? TestCase { get; set; }
        public string DefectId { get; set; } =string.Empty;
        public string BugzillaId { get; set; } = string.Empty;
        public string Component { get; set; } = string.Empty;
        public  BugAndDefectSeverity BugAndDefectSeverity {get; set;} 
        public BugAndDefectStatus BugAndDefectStatus { get; set;}
        public string? ExpectedResult { get; set; }
        public string? ActualResult { get; set; }
        public string? Resulation { get; set; }
        public string? DefectedSummary { get; set; }
        public string? StepstoReproduce {  get; set; }
        public long ReportedBy { get; set; }
        public User? User { get; set; }
        public DateTime? ReportedDate { get; set; }
        public List<BugAndDefectFile>? BugAndDefectFiles { get; set; }
    }
}
