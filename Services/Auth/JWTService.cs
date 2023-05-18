using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly string _jwtSecret;
        private readonly double _jwtExpirationMinutes;

        public JwtService(IConfiguration configuration)
        {
            _jwtSecret = configuration["Jwt:Secret"];
            _jwtExpirationMinutes = Convert.ToDouble(configuration["Jwt:ExpirationMinutes"]);
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("Login", user.Login),
            new Claim("Email", user.Email)
            // Add additional claims as needed
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}