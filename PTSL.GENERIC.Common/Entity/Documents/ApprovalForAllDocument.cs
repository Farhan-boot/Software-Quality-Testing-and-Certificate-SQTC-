using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Documents
{
    public class ApprovalForAllDocument : BaseEntity
    {
        public long AllTypesOfDocumentId {  get; set; }
        public long? SenderId { get; set; }
        public long? ReceiverId { get; set; }
        public DateTime? SendingDate { get; set; }
        public string? Remarks { get; set; }
        public long? SenderRoleId { get; set; }
        public string Description { get; set; } = string.Empty;
        public ProcessFlowType ProcessFlowType { get; set; }
        public DocumentType? DocumentType { get; set; }
        public TestingType? TestingType { get; set; }
        public long? PermissionRowSettingsId { get; set; }
        public ApprovalStatusForPdf StatusForPdf { get; set; }
        public bool IsAmmendment {  get; set; }
        public PermissionRowSettings? PermissionRowSettings { get; set; }
        public AllTypesOfDocument? AllTypesOfDocument { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
        public UserRole? SenderRole { get; set; }
    }
}
