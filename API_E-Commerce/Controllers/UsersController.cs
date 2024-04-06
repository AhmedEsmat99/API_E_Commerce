using API_E_Commerce.Model;
using API_E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IRepoUsers UserRepo;
        public UsersController(IRepoUsers _UserRepository)
        {
            this.UserRepo = _UserRepository;
        }
        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            ApplicationUser user = UserRepo.GetById(id);
            if (user != null)
                return Ok(user);
            else
                return BadRequest("User with this Id does not exist");
        }

        [HttpGet("GetByUsername")]
        public IActionResult GetByUsername(string username)
        {
            var user = UserRepo.GetByName( username);
            if (user != null)
                return Ok(user);
            else
                return BadRequest("User with this username does not exist");
        }
        [HttpGet("GetByEmail")]
        public IActionResult GetByEmail(string Email)
        {
            var user = UserRepo.GetByEmail(Email);
            if (user != null)
                return Ok(user);
            else
                return BadRequest("User with this Email does not exist");
        }
        [HttpGet("GetRoleByEmail")]
        public async Task<IActionResult> GetRoleByEmail(string Email)
        {
            var result = await UserRepo.GetRoleByEmail(Email);
            if (result != null)
                return Ok(result);
            else
                return BadRequest("User with this Email does not exist");
        }
    }
}
