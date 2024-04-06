using System.ComponentModel.DataAnnotations;

namespace API_E_Commerce.DTO
{
    public class RoleDTO
    {
        [Required]
        public string NameRole { get; set; }
    }
}
