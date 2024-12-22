using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using System;

namespace PTSL.GENERIC.Common.Entity.Sqtc_ClientLog
{
    public class ApprovalForRegisteredClientLog : BaseEntity
    {
        public long? ClientID { get; set; }
        public long? SenderId { get; set; }
        public User? Sender { get; set; }

        public long? ReceiverId { get; set; }
        public User? Receiver { get; set; }

        public DateTime? SendingDate { get; set; }
        public DateTime? SendingTime { get; set; }
        public string? Remark { get; set; }
        //New Fild add
        public long? SenderRoleId { get; set; }
        public UserRole? SenderRole { get; set; }
        public string Description { get; set; } = string.Empty;
        public ProcessFlowType ProcessFlowType { get; set; }
        public long? PermissionRowSettingsId {  get; set; }
        public PermissionRowSettings? PermissionRowSettings { get; set; }
    }
}

