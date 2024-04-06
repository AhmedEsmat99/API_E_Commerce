using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_E_Commerce.Model
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context() : base()
        {

        }
        public Context(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
