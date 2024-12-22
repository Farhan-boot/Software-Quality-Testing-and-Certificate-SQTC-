using System;

namespace PTSL.GENERIC.Common.Entity;

public interface IApprovalEntity
{
    public long? ApprovedById { get; set; }
    public User? ApprovedBy { get; set; }
    public long? ForwardedToId { get; set; }
    public User? ForwardedTo { get; set; }

    public long? ApprovedByRoleId { get; set; }
    public UserRole? ApprovedByRole { get; set; }
    public long? ForwardedToRoleId { get; set; }
    public UserRole? ForwardedToRole { get; set; }

    public DateTime? SendingDateTime { get; set; }

    public string? Remark { get; set; }
}
