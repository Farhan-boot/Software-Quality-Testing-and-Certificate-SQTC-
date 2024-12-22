using System;
using System.Threading.Tasks;

using PTSL.GENERIC.Business.Businesses.Interface.SystemUser;
using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.DAL.Repositories.Interface.SystemUser;

namespace PTSL.GENERIC.Business.Businesses.Implementation.SystemUser;

public class RefreshTokenBusiness : IRefreshTokenBusiness
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenBusiness(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(long userId, DateTime expiresAt, bool rememberMe, string token)
    {
        return _refreshTokenRepository.CreateAsync(userId, expiresAt, rememberMe, token);
    }

    public Task<(ExecutionState executionState, RefreshToken entity, string message)> GetAsync(long userId, string token)
    {
        return _refreshTokenRepository.GetAsync(userId, token);
    }

    public Task<(ExecutionState executionState, RefreshToken entity, string message)> GetByTokenAsync(string token)
    {
        return _refreshTokenRepository.GetByTokenAsync(token);
    }

    public Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync()
    {
        return _refreshTokenRepository.RemoveOutdatedAsync();
    }

    public Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, long userId)
    {
        return _refreshTokenRepository.RevokeAsync(token, userId);
    }

    public Task<(ExecutionState executionState, RefreshToken entity, string message)> UpdateAsync(RefreshToken token)
    {
        return _refreshTokenRepository.UpdateAsync(token);
    }
}