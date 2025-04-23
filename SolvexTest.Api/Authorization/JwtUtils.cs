using Microsoft.IdentityModel.Tokens;
using SolvexTest.Application.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SolvexTest.Api.Authorization
{
    public interface IJwtUtils
    {
        public int? ValidateToken(string token);
        List<Claim>? GetCurrentToken(string jwt);
        string GetClaim(string token, string claimType);
    }

    public class JwtUtils : IJwtUtils
    {
        IConfiguration _configuration;

        public JwtUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration.GetValue<string>("Jwt:Key");
            var key = Encoding.ASCII.GetBytes(secret!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }

        public List<Claim>? GetCurrentToken(string jwt)
        {
            if (string.IsNullOrEmpty(jwt)) return null;

            jwt = jwt.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return token.Claims.ToList();
        }

        public string GetClaim(string token, string claimType)
        {
            try
            {
                token = token.Replace("Bearer ", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                return securityToken!.Claims.First(claim => claim.Type == claimType).Value;
            }
            catch (Exception ex)
            {
                string message = "";
                if (ex.InnerException == null)
                { message += ex.Message; }
                else { message += ex.InnerException.Message; }

                throw new InternalServerError(message, ex);
            }
        }
    }
}