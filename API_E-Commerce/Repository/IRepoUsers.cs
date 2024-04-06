using API_E_Commerce.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_E_Commerce.Repository
{
    public interface IRepoUsers
    {
        ApplicationUser GetByEmail(string Email);
        ApplicationUser GetById(string id);
        ApplicationUser GetByName(string Name);
        Task<string> GetRoleByEmail(string Email);
    }
}