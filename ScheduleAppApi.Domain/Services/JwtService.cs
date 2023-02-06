using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScheduleAppApi.Domain.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScheduleAppApi.Domain.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        public JwtService(IConfiguration conf) 
            => _secretKey = conf.GetValue<string>("ApiSettings:Secret");
        /// <summary>
        /// Creates Jwt token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns>Token</returns>
        public string GetJwtToken(int userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
