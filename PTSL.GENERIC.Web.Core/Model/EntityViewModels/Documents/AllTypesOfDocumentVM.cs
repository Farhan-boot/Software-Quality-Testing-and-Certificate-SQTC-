
using PTSL.GENERIC.Web.Core.Enum.Documents;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.Documents;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents
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
        public string? ApprovalMessage { get; set; }
        public bool IsApprovalShow { get; set; }
        public bool IsAcceptOrReject { get; set; }
        public bool HasEditAndDltPrmsn {  get; set; }
        public string RejectionComment { get; set; } = string.Empty;
        public string ViewVersionNo { get; set; } = string.Empty;
        public DocumentAmendmentState DocumentAmendmentState { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
    }
}
