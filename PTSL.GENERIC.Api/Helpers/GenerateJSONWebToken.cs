using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using PTSL.GENERIC.Business.TokenHelper;
using PTSL.GENERIC.Common.Model;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PTSL.GENERIC.Api.Helpers
{
    public class GenerateJSONWebToken : IGenerateJSONWebToken
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static readonly object BufferLock = new();
        private static readonly byte[] RefreshTokenBuffer = new byte[64];

        public GenerateJSONWebToken(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        public string GenerateRefreshToken()
        {
            using var rng = RandomNumberGenerator.Create();

            lock (BufferLock)
            {
                rng.GetBytes(RefreshTokenBuffer);
                return Convert.ToBase64String(RefreshTokenBuffer);
            }
        }

        public string GetToken(UserVM userInfo, bool rememberMe)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new Exception("Jwt key could be loaded from appsettings.json")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Name, userInfo?.UserName ?? string.Empty),
                new Claim(CustomClaimTypes.AuthTime, DateTime.Now.ToString()),
                new Claim(CustomClaimTypes.RememberMe, rememberMe.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CustomClaimTypes.UserId, userInfo.Id.ToString()),
            };

            var expires = _webHostEnvironment.IsDevelopment() ? DateTime.Now.AddHours(12) : DateTime.Now.AddMinutes(_config.GetValue<int>("Jwt:TokenExpireInMinutes"));
            var token = new JwtSecurityToken
                (
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
