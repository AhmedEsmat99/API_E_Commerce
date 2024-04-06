using API_E_Commerce.Model;
using System.ComponentModel.DataAnnotations;

namespace API_E_Commerce.DTO
{
    public class RegisterDTO 
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
