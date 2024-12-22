using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.SecurityTestings
{
    public class SecurityTestingFileVM : BaseModel
    {
        public long SecurityTestingId { get; set; }
        public SecurityTestingVM? SecurityTesting { get; set; }

        public string? FileName { get; set; }
        public string? FileNameUrl { get; set; }
    }
}
