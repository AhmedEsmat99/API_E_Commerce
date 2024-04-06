using System.Collections.Generic;
using System;
using API_E_Commerce.Model;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API_E_Commerce.Repository
{
    public class RepoUsers : IRepoUsers
    {
         private readonly UserManager<ApplicationUser> userManager;
        Context db;
        public RepoUsers(Context context, UserManager<ApplicationUser> _userManager)
        {
            this.db = context;
            this.userManager =_userManager;
        }
        public ApplicationUser GetById(string id)
        {
            var Entity = db.AspNetUsers.FirstOrDefault(a => a.Id == id);
            return Entity;
        }
        public ApplicationUser GetByName(string Name)
        {
            var Entity = db.AspNetUsers.FirstOrDefault(a => a.UserName == Name);
            return Entity;
        }
        public ApplicationUser GetByEmail(string Email)
        {
            var Entity = db.AspNetUsers.FirstOrDefault(a => a.Email == Email);
            return Entity;
        }
        public async Task<string> GetRoleByEmail(string Email)
        {
            var user = GetByEmail(Email);
            if (user == null)
                return null;
            else
            {
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Any())
                    return roles.First();
                else
                    return null;
            }
        }
    }
}
