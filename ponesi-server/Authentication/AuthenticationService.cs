using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PonesiWebApi.FunctionalResult;
using PonesiWebApi.Interfaces;
using PonesiWebApi.Models;

namespace PonesiWebApi.Authentication
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationConfiguration _configuration;


        public AuthenticationService(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;

        }

    public Result<string> GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "User")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.PrivateKey));
        if(key == null)
        {
            return Result<string>.Failure(ConfigurationErrors.ConfigurationLinkMissingError);
        }
         
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration.ExpireDays));
        if(expires == null)
        {
            return Result<string>.Failure(ConfigurationErrors.ConfigurationLinkMissingError);
        }

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(token));
    }


    }
}
