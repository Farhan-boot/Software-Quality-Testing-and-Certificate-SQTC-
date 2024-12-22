using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.SecurityTestings
{
    public class SecurityTestingFileVM : BaseModel
    {
        public long SecurityTestingId { get; set; }
        public SecurityTestingVM? SecurityTesting { get; set; }

        public string? FileName { get; set; }
        public string? FileNameUrl { get; set; }
    }
}
