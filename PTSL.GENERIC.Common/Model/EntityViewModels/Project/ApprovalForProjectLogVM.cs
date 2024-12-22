using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings;
using System;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class ApprovalForProjectLogVM : BaseModel
    {
        public long? ProjectID { get; set; }
        public long? SenderId { get; set; }
        public UserVM? Sender { get; set; }

        public long? ReceiverId { get; set; }
        public UserVM? Receiver { get; set; }

        public DateTime? SendingDate { get; set; }
        public DateTime? SendingTime { get; set; }
        public string? Remark { get; set; }
        //New Fild add
        public long? SenderRoleId { get; set; }
        public UserRoleVM? SenderRole { get; set; }
        public string Description { get; set; } = string.Empty;
        public ProcessFlowType ProcessFlowType { get; set; }
        public long? PermissionRowSettingsId { get; set; }
        public PermissionRowSettingsVM? PermissionRowSettingsVM { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
    }
}
