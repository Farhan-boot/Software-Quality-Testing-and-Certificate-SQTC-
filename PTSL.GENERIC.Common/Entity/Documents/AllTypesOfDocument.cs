using Microsoft.EntityFrameworkCore.Query.Internal;
using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Documents
{
    public class AllTypesOfDocument : BaseEntity
    {
        public long ProjectRequestId { get; set; }
        public DocumentType DocumentType { get; set; }
        public TestingType TestingType { get; set; }
        public DocumentApprovalStatus DocumentApprovalStatus { get; set; }
        public string EditorContent { get; set; } = string.Empty;
        public string VersionNo { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string RejectionComment {  get; set; } = string.Empty;
        public string ViewVersionNo {  get; set; } = string.Empty;
        public DocumentAmendmentState DocumentAmendmentState { get; set; }
        public ProjectRequest ProjectRequest { get; set; }
    }
}
