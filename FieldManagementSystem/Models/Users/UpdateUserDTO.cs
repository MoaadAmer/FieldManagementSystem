using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystemAPI.Models.Users
{
    public class UpdateUserDTO
    {
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
