using PTSL.GENERIC.Common.Model;

namespace PTSL.GENERIC.Business.TokenHelper;

public interface IGenerateJSONWebToken
{
    string GenerateRefreshToken();
    string GetToken(UserVM? userInfo, bool rememberMe);
}