using System;

namespace PTSL.GENERIC.Common.Entity.UserEntitys;

public class RefreshToken
{
    public string Id { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool RememberMe { get; set; }
    public bool IsRevoked { get; set; }
    public string? DeviceInfo { get; set; }

    public RefreshToken()
    {
        Id = Guid.NewGuid().ToString();
    }
}
