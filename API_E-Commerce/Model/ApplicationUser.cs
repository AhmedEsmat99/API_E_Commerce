using Microsoft.AspNetCore.Identity;

namespace API_E_Commerce.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePictureUrl { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        
    }
}
