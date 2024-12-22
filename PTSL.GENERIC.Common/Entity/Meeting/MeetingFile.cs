using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.Meetings
{
    public class MeetingFiles : BaseEntity
    {
        public long MeetingId { get; set; }
        public Meeting? Meeting { get; set; }
        public string? FileName { get; set; }
        public string? FileNameUrl { get; set; }
    }
}

