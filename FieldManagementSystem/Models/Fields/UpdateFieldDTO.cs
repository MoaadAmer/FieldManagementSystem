using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystemAPI.Models.Fields
{
    public class UpdateFieldDTO
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)] 
        public string Location { get; set; }
    }
}
