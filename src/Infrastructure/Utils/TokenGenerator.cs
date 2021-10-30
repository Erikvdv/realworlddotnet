using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;
using Realworlddotnet.Infrastructure.Utils.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Realworlddotnet.Infrastructure.Utils;

public class TokenGenerator : ITokenGenerator
{
    private readonly RsaSecurityKey? _rsaSecurityKey;

    public TokenGenerator(X509Certificate2 certificate)
    {
        var privateKey = certificate.GetRSAPrivateKey();
        _rsaSecurityKey = new RsaSecurityKey(privateKey);
    }

    public string CreateToken(string username)
    {
        var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, username) };

        var handler = new JwtSecurityTokenHandler();
        var token = new JwtSecurityToken(
            "theclientid",
            "https://AAAS_PLATFORM/idp/YOUR_TENANT/authn/token",
            claims,
            DateTime.UtcNow.AddMilliseconds(-30),
            DateTime.UtcNow.AddMinutes(60),
            new SigningCredentials(_rsaSecurityKey, SecurityAlgorithms.RsaSha256));

        return handler.WriteToken(token);
    }
}
