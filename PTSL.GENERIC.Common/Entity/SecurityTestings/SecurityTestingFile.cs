using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.Entity.SecurityTestings
{
    public class SecurityTestingFile : BaseEntity
    {
        public long SecurityTestingId { get; set; }
        public SecurityTesting? SecurityTesting { get; set; }

        public string? FileName { get; set; }
        public string? FileNameUrl { get; set; }
    }
}
