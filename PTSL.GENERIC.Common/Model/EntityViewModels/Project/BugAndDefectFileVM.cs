using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class BugAndDefectFileVM : BaseModel
    {
        public long BugAndDefectId { get; set; }
        public BugAndDefectVM? BugAndDefect { get; set; }

        public string? FileName { get; set; }
        public string? FileNameUrl { get; set; }
    }
}
