using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using kanban_net.Models;
using Microsoft.IdentityModel.Tokens;

namespace kanban_net.Services
{
    public class JwtService(IConfiguration configuration)
    {
        private readonly string secret = configuration["Jwt:Key"] ?? throw new Exception("JWT secret missing in configuration");

        public string GenerateToken(User user, int expirationMinutes = 60)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                    ]
                ),

                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}