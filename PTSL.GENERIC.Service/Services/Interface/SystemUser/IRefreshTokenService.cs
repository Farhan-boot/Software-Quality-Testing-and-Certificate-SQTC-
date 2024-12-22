using System;
using System.Threading.Tasks;

using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;

namespace PTSL.GENERIC.Service.Services.Interface.SystemUser;

public interface IRefreshTokenService
{
    Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, long userId);
    Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync();
    Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(long userId, bool rememberMe, string token);
    Task<(ExecutionState executionState, LoginResultVM? entity, string message)> GetNewTokenAsync(UserTokenRequestResponseVM model);
}