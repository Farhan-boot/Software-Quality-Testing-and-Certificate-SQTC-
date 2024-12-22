

using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Meetings
{
    public class MeetingFilesVM : BaseModel
    {
        public long MeetingId { get; set; }
        public MeetingVM? Meeting { get; set; }
        public string? FileName { get; set; }
        public string? FileNameUrl { get; set; }
    }
}

