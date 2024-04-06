using System.ComponentModel.DataAnnotations;

namespace API_E_Commerce.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
