using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystemAPI.Models.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
