using PTSL.GENERIC.Web.Core.Helper.Enum.Documents;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents
{
    public class DocumentsByTypeVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public long? ClientId { get; set; }
        public long? MeetingId { get; set; }
        public long DocumentCategoriesId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public DocumentModuleType DocumentModuleType { get; set; }
        public string DocumentPurpose { get; set; } = string.Empty;
        public string DocumentTitle { get; set; } = string.Empty;
        public string DoumentPath { get; set; } = string.Empty;
        public string DocumentUniqueName { get; set; } = string.Empty;
        //public IFormFile? DocFile { get; set; }
        public ClientVM? Client { get; set; }
        public MeetingVM? Meeting { get; set; }
        public DocumentCategoriesVM? DocumentCategories { get; set; }
    }
}
