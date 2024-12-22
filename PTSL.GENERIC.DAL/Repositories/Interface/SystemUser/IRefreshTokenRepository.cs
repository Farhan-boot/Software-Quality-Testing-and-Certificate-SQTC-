using System;
using System.Threading.Tasks;

using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.Common.Enum;

namespace PTSL.GENERIC.DAL.Repositories.Interface.SystemUser
{
    public interface IRefreshTokenRepository
    {
        Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, long userId);
        Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync();
        Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(long userId, DateTime expiresAt, bool rememberMe, string token);
        Task<(ExecutionState executionState, RefreshToken entity, string message)> UpdateAsync(RefreshToken toke);
        Task<(ExecutionState executionState, RefreshToken entity, string message)> GetAsync(long userId, string token);
        Task<(ExecutionState executionState, RefreshToken entity, string message)> GetByTokenAsync(string token);
    }
}