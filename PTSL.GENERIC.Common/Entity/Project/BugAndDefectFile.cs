using Humanizer;
using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class BugAndDefectFile : BaseEntity
    {
        public long BugAndDefectId { get; set; }
        public BugAndDefect? BugAndDefect { get; set; }

        public string? FileName { get; set; }
        public string? FileNameUrl { get; set; }
    }
}
