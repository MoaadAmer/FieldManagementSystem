using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystemAPI.Models.Users
{
    public class AddUserDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
