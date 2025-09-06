using FieldManagementSystemAPI.Entites;
using FieldManagementSystemAPI.Models.Fields;
using FieldManagementSystemAPI.Repositories.Fields;
using FieldManagementSystemAPI.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldManagementSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FieldsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFieldRepository _fieldRepository;

        public FieldsController(IUserRepository userRepository, IFieldRepository fieldRepository)

        {
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFieldDTO field)
        {
            if (await _userRepository.GetById(field.UserId) == null)
            {
                return BadRequest("Invalid UserId");
            }
            var newField = new Field() { Name = field.Name, Location = field.Location, UserId = field.UserId };
            await _fieldRepository.Add(newField);
            return CreatedAtAction(nameof(GetById), new { Id = newField.Id }, newField);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Field>> GetById(int id)
        {
            Field? field = await _fieldRepository.GetById(id);
            if (field == null)
            {
                return NotFound();
            }
            return Ok(field);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Field>>> GetByUserId(int userId)
        {
            if (await _userRepository.GetById(userId) == null)
            {
                return NotFound();
            }
            return Ok(await _fieldRepository.GetByUserId(userId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateFieldDTO field)
        {
            if (await _fieldRepository.GetById(id) == null)
            {
                return NotFound();
            }
            var newField = new Field() { Name = field.Name, Location = field.Location };
            await _fieldRepository.Update(id, newField);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Field? field = await _fieldRepository.GetById(id);
            if (field == null)
            {
                return NotFound();
            }
            await _fieldRepository.Delete(id);
            return NoContent();
        }
    }
}
