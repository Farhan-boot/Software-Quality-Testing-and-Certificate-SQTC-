using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum.Documents;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Documents
{
    public class AllTypesOfDocumentVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public DocumentType DocumentType { get; set; }
        public TestingType TestingType { get; set; }
        public DocumentApprovalStatus DocumentApprovalStatus { get; set; }
        public string EditorContent { get; set; } = string.Empty;
        public string VersionNo { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string RejectionComment { get; set; } = string.Empty;
        public string ViewVersionNo { get; set; } = string.Empty;
        public ProjectRequestVM? ProjectRequest { get; set; }
        public DocumentAmendmentState DocumentAmendmentState { get; set; }
    }
}
