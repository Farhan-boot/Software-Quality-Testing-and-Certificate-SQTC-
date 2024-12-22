using PTSL.GENERIC.Web.Core.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Enum.Documents;
using PTSL.GENERIC.Web.Core.Helper.Enum.Documents;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents
{
    public class ApprovalForAllDocumentVM : BaseModel
    {
        public long AllTypesOfDocumentId { get; set; }
        public long? SenderId { get; set; }
        public long? ReceiverId { get; set; }
        public DateTime? SendingDate { get; set; }
        public string? Remarks { get; set; }
        public long? SenderRoleId { get; set; }
        public string Description { get; set; } = string.Empty;
        public ProcessFlowType ProcessFlowType { get; set; }
        public DocumentType? DocumentType { get; set; }
        public TestingType? TestingType { get; set; }
        public string? SenderName { get; set; } = string.Empty;
        public string? ReceiverName { get; set; } = string.Empty;
        public long? PermissionRowSettingsId { get; set; }
        public DocumentApprovalStatus? DocumentApprovalStatus { get; set; }
        public ApprovalStatusForPdf StatusForPdf { get; set; }
        public bool IsAmmendment { get; set; }
        public PermissionRowSettingsVM? PermissionRowSettings { get; set; }
        public AllTypesOfDocumentVM? AllTypesOfDocument { get; set; }
        public UserVM? Sender { get; set; }
        public UserVM? Receiver { get; set; }
        public UserRoleVM? SenderRole { get; set; }
    }
}
