using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystemAPI.Models.Controllers
{
    public class AddControllerDTO
    {
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        public int FieldId { get; set; }
    }
}
