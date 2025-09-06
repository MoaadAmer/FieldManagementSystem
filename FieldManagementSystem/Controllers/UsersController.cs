using FieldManagementSystemAPI.Entites;
using FieldManagementSystemAPI.Models.Users;
using FieldManagementSystemAPI.Repositories.Roles;
using FieldManagementSystemAPI.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldManagementSystemAPI.Controllers
{
    [Authorize(Roles="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository repository
            , IRoleRepository roleRepository)
        {
            _logger = logger;
            _userRepository = repository;
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add(AddUserDTO addUserDto)
        {
            if (await _roleRepository.GetById(addUserDto.RoleId) == null)
            {
                return BadRequest("Invalid RoleId");
            }
            if (await _userRepository.GetByEmail(addUserDto.Email) != null)
            {
                return BadRequest("user with this email already exists");
            }
            var newUser = new User() { Email = addUserDto.Email, RoleId = addUserDto.RoleId };
            await _userRepository.Add(newUser);
            return CreatedAtAction(nameof(GetById), new { Id = newUser.Id }, newUser);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _userRepository.GetAll());
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            User? user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            User? user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDTO UpdateUserDto)
        {
            if (await _userRepository.GetById(id) == null)
            {
                return NotFound();
            }
            if (await _userRepository.GetByEmail(UpdateUserDto.Email) != null)
            {
                return BadRequest("User with this email already exists");
            }
            var updateUser = new User() { Email = UpdateUserDto.Email };
            await _userRepository.Update(id, updateUser);
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
