using FieldManagementSystemAPI.Models.Controllers;
using FieldManagementSystemAPI.Repositories.Contorllers;
using FieldManagementSystemAPI.Repositories.Fields;
using Microsoft.AspNetCore.Mvc;
using Controller = FieldManagementSystemAPI.Entites.Controller;

namespace FieldManagementSystemAPI.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddControllerDTO controller)
        {
            if (await _fieldRepository.GetById(controller.FieldId) == null)
            {
                return BadRequest("Invalid field id");
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Controller? controller = await _controllerRepository.GetById(id);
            if (controller == null)
            {
                return NotFound();
            }
            await _controllerRepository.Delete(id);
            return NoContent();
        }

    }
}
