using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Helper.ProjectPackageConfiguration;
using System;

namespace PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration
{
    public class ReviewComment : BaseEntity
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

