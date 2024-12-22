using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.DAL.Repositories.Interface.SystemUser;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.SystemUser
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        private readonly GENERICWriteOnlyCtx _writeOnlyCtx;

        public RefreshTokenRepository(GENERICReadOnlyCtx readOnlyCtx, GENERICWriteOnlyCtx writeOnlyCtx)
        {
            _readOnlyCtx = readOnlyCtx;
            _writeOnlyCtx = writeOnlyCtx;
        }

        public async Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(long userId, DateTime expiresAt, bool rememberMe, string token)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                RememberMe = rememberMe,
                CreatedAt = DateTime.Now,
                ExpiresAt = expiresAt,
            };

            _writeOnlyCtx.Set<RefreshToken>().Add(refreshToken);
            await _writeOnlyCtx.SaveChangesAsync();

            return (ExecutionState.Success, refreshToken, "Success");
        }

        public async Task<(ExecutionState executionState, RefreshToken entity, string message)> GetByTokenAsync(string token)
        {
            var result = await _readOnlyCtx.Set<RefreshToken>().Where(x => x.Token == token).FirstOrDefaultAsync();
            if (result is null)
            {
                return (ExecutionState.Failure, null, "");
            }

            return (ExecutionState.Success, result, "Success");
        }

        public async Task<(ExecutionState executionState, RefreshToken entity, string message)> GetAsync(long userId, string token)
        {
            var result = await _readOnlyCtx.Set<RefreshToken>().Where(x => x.UserId == userId && x.Token == token).FirstOrDefaultAsync();
            if (result is null)
            {
                return (ExecutionState.Failure, null, "");
            }

            return (ExecutionState.Success, result, "Success");
        }

        public async Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync()
        {
            var rowsEffected = await _writeOnlyCtx.Set<RefreshToken>()
                .Where(x => x.ExpiresAt < DateTime.Now)
                .ExecuteDeleteAsync();

            return (rowsEffected > 0 ? ExecutionState.Success : ExecutionState.Failure, rowsEffected > 0 ? true : false, "Done");
        }

        public async Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, long userId)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return (ExecutionState.Failure, false, "Token is empty");
            }

            var refreshToken = await _writeOnlyCtx.Set<RefreshToken>().FirstOrDefaultAsync(x => x.Token == token && x.UserId == userId);
            if (refreshToken == null)
            {
                return (ExecutionState.Failure, false, "Not found");
            }

            refreshToken.IsRevoked = true;
            await _writeOnlyCtx.SaveChangesAsync();

            return (ExecutionState.Success, true, "Done");
        }

        public async Task<(ExecutionState executionState, RefreshToken entity, string message)> UpdateAsync(RefreshToken token)
        {
            _writeOnlyCtx.Set<RefreshToken>().Update(token);
            await _writeOnlyCtx.SaveChangesAsync();

            return (ExecutionState.Success, token, "Update done");
        }
    }
}