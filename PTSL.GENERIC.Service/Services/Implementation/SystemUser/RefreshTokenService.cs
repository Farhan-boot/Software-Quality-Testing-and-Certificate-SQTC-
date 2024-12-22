using System;
using System.Threading.Tasks;

using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Business.Businesses.Interface.SystemUser;
using PTSL.GENERIC.Business.TokenHelper;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Service.Services.Interface.SystemUser;

namespace PTSL.GENERIC.Service.Services.Implementation.SystemUser
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenBusiness _business;
        public IMapper _mapper;
        private readonly IUserBusiness _userBusiness;
        private readonly IGenerateJSONWebToken _generateJSONWebToken;
        private readonly IRefreshTokenBusiness _refreshTokenBusiness;

        public RefreshTokenService(IRefreshTokenBusiness business, IMapper mapper, IUserBusiness userBusiness, IGenerateJSONWebToken generateJSONWebToken, IRefreshTokenBusiness refreshTokenBusiness)
        {
            _business = business;
            _mapper = mapper;
            _userBusiness = userBusiness;
            _generateJSONWebToken = generateJSONWebToken;
            _refreshTokenBusiness = refreshTokenBusiness;
        }

        private static DateTime GetRefreshTokenExpire(bool rememberMe)
        {
            return rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddDays(1);
        }

        public Task<(ExecutionState executionState, RefreshToken entity, string message)> CreateAsync(long userId, bool rememberMe, string token)
        {
            var expiresAt = GetRefreshTokenExpire(rememberMe);

            return _business.CreateAsync(userId, expiresAt, rememberMe, token);
        }

        public async Task<(ExecutionState executionState, LoginResultVM? entity, string message)> GetNewTokenAsync(UserTokenRequestResponseVM model)
        {
            (ExecutionState executionState, RefreshToken entity, string message) refreshToken;
            (ExecutionState executionState, User entity, string message) user;

            if (string.IsNullOrWhiteSpace(model.AccessToken) == false)
            {
                var userIdClaim = TokenDecoder.GetClaim(model.AccessToken, CustomClaimTypes.UserId);
                if (userIdClaim is null)
                {
                    return (ExecutionState.Failure, null, "Invalid access token");
                }

                var isRememberMeClaim = TokenDecoder.GetClaim(model.AccessToken, CustomClaimTypes.RememberMe);
                if (isRememberMeClaim is null)
                {
                    return (ExecutionState.Failure, null, "Invalid access token");
                }

                var userIdString = userIdClaim.Value;
                _ = long.TryParse(userIdString, out var userId);

                refreshToken = await _refreshTokenBusiness.GetAsync(userId, model.RefreshToken);
                if (refreshToken.entity is null)
                {
                    return (ExecutionState.Failure, null, "Token not found");
                }

                if (refreshToken.entity.ExpiresAt < DateTime.Now)
                {
                    return (ExecutionState.Failure, null, "Token expired");
                }

                user = await _userBusiness.GetAsync(userId);
            }
            else
            {
                refreshToken = await _refreshTokenBusiness.GetByTokenAsync(model.RefreshToken);
                if (refreshToken.entity is null)
                {
                    return (ExecutionState.Failure, null, "Token not found");
                }

                if (refreshToken.entity.ExpiresAt < DateTime.Now)
                {
                    return (ExecutionState.Failure, null, "Token expired");
                }

                user = await _userBusiness.GetAsync(refreshToken.entity.UserId);
            }

            if (user.entity is null)
            {
                return (ExecutionState.Failure, null, "User not found");
            }

            refreshToken.entity.ExpiresAt = GetRefreshTokenExpire(refreshToken.entity.RememberMe);

            await _refreshTokenBusiness.UpdateAsync(refreshToken.entity);

            var generatedAccessToken = _generateJSONWebToken.GetToken(_mapper.Map<UserVM>(user.entity), refreshToken.entity.RememberMe);
            var result = new LoginResultVM
            {
                UserId = user.entity.Id,
                UserName = user.entity.UserName,
                UserEmail = user.entity.UserEmail,
                Token = generatedAccessToken,
                AccessToken = generatedAccessToken,
                RefreshToken = refreshToken.entity.Token,
            };
            return (ExecutionState.Success, result, "Done");
        }

        public Task<(ExecutionState executionState, bool entity, string message)> RemoveOutdatedAsync()
        {
            return _business.RemoveOutdatedAsync();
        }

        public Task<(ExecutionState executionState, bool entity, string message)> RevokeAsync(string token, long userId)
        {
            return _business.RevokeAsync(token, userId);
        }
    }
}