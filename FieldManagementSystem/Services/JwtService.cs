using FieldManagementSystemAPI.Entites;
using FieldManagementSystemAPI.Repositories.Roles;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FieldManagementSystemAPI.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        private readonly IRoleRepository _roleRepository;

        public JwtService(IConfiguration configuration,IRoleRepository roleRepository)
        {
            _config = configuration;
            _roleRepository = roleRepository;
        }
        public async Task<string> GenerateToken(User user)
        {
            // Get role name from RoleRepository
            Role? role = await _roleRepository.GetById(user.RoleId);
            string roleName = role?.Name ?? "Unknown";

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
        new Claim(ClaimTypes.Email, user.Email),                  
        new Claim(ClaimTypes.Role, roleName)                      
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
