namespace PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;

public record struct UserTokenRequestResponseVM(string RefreshToken, string? AccessToken = null);

