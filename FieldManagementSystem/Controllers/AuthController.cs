using FieldManagementSystemAPI.Entites;
using FieldManagementSystemAPI.Models.Auth;
using FieldManagementSystemAPI.Repositories.Users;
using FieldManagementSystemAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FieldManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            User? user = await _userRepository.GetByEmail(login.Email);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, login.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized();
            }

            var token = await _jwtService.GenerateToken(user);
            return Ok(token);
        }
    }
}
