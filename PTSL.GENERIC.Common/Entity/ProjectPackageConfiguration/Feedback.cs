using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class Feedback : BaseEntity
    {
        public long? UserId { get; set; }
        public long? ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public bool? IsApprove { get; set; }
        public string? Comments { get; set; }
        public long? RatingCount { get; set; }
    }
}

