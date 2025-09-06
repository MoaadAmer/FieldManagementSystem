using FieldManagementSystemAPI.Entites;
using FieldManagementSystemAPI.Models.Controllers;
using FieldManagementSystemAPI.Repositories.Contorllers;
using FieldManagementSystemAPI.Repositories.Fields;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Controller = FieldManagementSystemAPI.Entites.Controller;

namespace FieldManagementSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ControllersController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IControllerRepository _controllerRepository;

        public ControllersController(IFieldRepository fieldRepository, IControllerRepository controllerRepository)
        {
            _fieldRepository = fieldRepository;
            _controllerRepository = controllerRepository;
        }

        [Authorize(Roles = "Farmer")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddControllerDTO controller)
        {
            if (!await UserOwnsField(controller.FieldId))
            {
                return Forbid("You do not own the field.");
            }
            var newController = new Controller() { Type = controller.Type, FieldId = controller.FieldId };
            await _controllerRepository.Add(newController);
            return CreatedAtAction(nameof(GetById), new { Id = newController.Id }, newController);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Controller>> GetById(int id)
        {
            Controller? controller = await _controllerRepository.GetById(id);
            if (controller == null)
            {
                return NotFound();
            }
            return Ok(controller);
        }

        [HttpGet("field/{fieldId}")]
        public async Task<ActionResult<IEnumerable<Controller>>> GetByFieldId(int fieldId)
        {
            if (await _fieldRepository.GetById(fieldId) == null)
            {
                return NotFound();
            }
            return Ok(await _controllerRepository.GetByFieldId(fieldId));
        }
        [Authorize(Roles = "Farmer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Controller? controller = await _controllerRepository.GetById(id);
            if (controller == null)
            {
                return NotFound();
            }

            if (!await UserOwnsField(controller.FieldId))
            {
                return Forbid("You do not own the field this controller belongs to.");
            }
            await _controllerRepository.Delete(id);
            return NoContent();
        }



        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        private async Task<bool> UserOwnsField(int fieldId)
        {
            Field? field = await _fieldRepository.GetById(fieldId);
            if (field == null) return false;

            int currentUserId = GetCurrentUserId();
            return field.UserId == currentUserId;
        }

    }
}
