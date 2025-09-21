using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystemAPI.Models.Fields
{
    public class AddFieldDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]

        public string Location { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
