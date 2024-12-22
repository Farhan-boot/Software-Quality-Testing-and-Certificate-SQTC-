using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Documents
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
        public string DoumentUniqueName { get; set; } = string.Empty;
        public string DoumentPath { get; set; } = string.Empty;
        public ClientVM? Client { get; set; }
        public MeetingVM? Meeting { get; set; }
        public DocumentCategoriesVM? DocumentCategories { get; set; }
    }
}
