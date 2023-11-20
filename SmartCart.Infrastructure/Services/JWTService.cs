using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SmartCart.Application.Contracts;
using SmartCart.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SmartCart.Infrastructure.Services
{
    public class JWTService(RoleManager<IdentityRole> roleManager) : IJWTService
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<string> GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x7UgVrY2nKP9qXBtjDZRsG4Z6E8HygT1"));

            // Create the token claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.UserName),
                new (ClaimTypes.Email, user.Email),
            };

            var roles = new List<string>();

            foreach (var userRole in user.UserRoles)
            {
                var role = await _roleManager.FindByIdAsync(userRole.RoleId);
                if (role != null)
                {
                    roles.Add(role.Name);
                }
            }

            claims.Add(new Claim("roles", JsonSerializer.Serialize(roles)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble("10")),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
