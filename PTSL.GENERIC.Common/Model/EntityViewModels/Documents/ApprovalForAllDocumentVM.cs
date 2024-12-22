using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum.Documents;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Documents
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
        public ApprovalStatusForPdf StatusForPdf { get; set; }
        public long? PermissionRowSettingsId { get; set; }
        public DocumentApprovalStatus? DocumentApprovalStatus { get; set; }
        public bool IsAmmendment { get; set; }
        public PermissionRowSettingsVM? PermissionRowSettings { get; set; }
        public AllTypesOfDocument? AllTypesOfDocument { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
        public UserRole? SenderRole { get; set; }
    }
}
