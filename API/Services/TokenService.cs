using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenkey = config["Tokenkey"] ?? throw new Exception("Cennot access tokenkey from appsettings");
        if (tokenkey.Length < 64) throw new Exception("Your tokenkey needs to be longer");

        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));

        var Claims = new List<Claim>
       {
        new(ClaimTypes.NameIdentifier,user.UserName)
       };
        var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(Claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds

        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var taken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(taken);
    }
}
