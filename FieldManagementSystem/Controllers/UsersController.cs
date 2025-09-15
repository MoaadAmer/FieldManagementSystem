using FieldManagementSystemAPI.Entites;
using FieldManagementSystemAPI.Models.Users;
using FieldManagementSystemAPI.Repositories.Roles;
using FieldManagementSystemAPI.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FieldManagementSystemAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRoleRepository _roleRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository repository
            , IPasswordHasher<User> passwordHasher, IRoleRepository roleRepository)
        {
            _logger = logger;
            _userRepository = repository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<ActionResult<GetUserDTO>> Add(AddUserDTO addUserDto)
        {
            if (await _roleRepository.GetById(addUserDto.RoleId) == null)
            {
                return BadRequest("Invalid RoleId");
            }
            if (await _userRepository.GetByEmail(addUserDto.Email) != null)
            {
                return BadRequest("user with this email already exists");
            }
            var newUser = new User()
            {
                Email = addUserDto.Email,
                RoleId = addUserDto.RoleId
            };
            newUser.HashedPassword = _passwordHasher.HashPassword(newUser, addUserDto.Password);
            await _userRepository.Add(newUser);

            var userResult = new GetUserDTO()
            {
                Id = newUser.Id,
                Email = newUser.Email,
                RoleId = newUser.RoleId,
            };
            return CreatedAtAction(nameof(GetById), new { Id = userResult.Id }, userResult);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetAll()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            return Ok(
                users.Select(
                    user => new GetUserDTO()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        RoleId = user.RoleId,
                    }
                    )
                );
        }
        [HttpGet("id/{id}")]
        public async Task<ActionResult<GetUserDTO>> GetById(int id)
        {
            User? user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new GetUserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                RoleId = user.RoleId,
            });
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<GetUserDTO>> GetByEmail(string email)
        {
            User? user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new GetUserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                RoleId = user.RoleId,
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDTO UpdateUserDto)
        {
            User userById = await _userRepository.GetById(id);
            if (userById == null)
            {
                return NotFound();
            }
            User userByEmail = await _userRepository.GetByEmail(UpdateUserDto.Email);
            if (userByEmail != null && userById.Email != userByEmail.Email)
            {
                return BadRequest("User with this email already exists");
            }
            var newUser = new User() { Email = UpdateUserDto.Email };
            newUser.HashedPassword = _passwordHasher.HashPassword(newUser, UpdateUserDto.Password);
            await _userRepository.Update(id, newUser);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User? user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.Delete(id);
            return NoContent();
        }
    }
}
