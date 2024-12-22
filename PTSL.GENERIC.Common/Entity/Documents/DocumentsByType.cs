using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Documents
{
    public class DocumentsByType : BaseEntity
    {
        public long ProjectRequestId { get; set; }
        public long? ClientId { get; set; }
        public long? MeetingId { get; set; }
        public long DocumentCategoriesId {  get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public DocumentModuleType DocumentModuleType { get; set; }
        public string DocumentPurpose { get; set; } = string.Empty;
        public string DocumentTitle { get; set; } = string.Empty;
        public string DoumentPath { get; set; } = string.Empty;
        public string DoumentUniqueName { get; set; } = string.Empty;
        public Client? Client { get; set; }
        public Meeting? Meeting { get; set; }
        public DocumentCategories? DocumentCategories { get; set; }
    }
}
