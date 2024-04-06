using API_E_Commerce.DTO;
using API_E_Commerce.Model;
using API_E_Commerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManger;
        ICRUD_Repository<IdentityRole> roleRepo;
        public RoleController(RoleManager<IdentityRole> _role,ICRUD_Repository<IdentityRole> dtorole)
        {
            this.roleManger = _role;
            this.roleRepo = dtorole;
        }
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = roleRepo.GetAll();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public IActionResult GetRoleById(string id)
        {
            var role = roleRepo.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleDTO Role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleIdentity = new IdentityRole();
                IdentityRole NameRole = roleRepo.GetByName(r => r.Name == Role.NameRole);
                if (NameRole == null)
                {
                    roleIdentity.Name = Role.NameRole;
                    IdentityResult result = await roleManger.CreateAsync(roleIdentity);
                    if (result.Succeeded)
                    {
                        return Ok("Save Data");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    return BadRequest("the name " + Role .NameRole+ " is already exits");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, RoleDTO Role)
        {
            var role = roleRepo.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = Role.NameRole;
                await roleManger.UpdateAsync(role);
                return Ok("Save Data");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = roleRepo.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                await roleManger.DeleteAsync(role);
                return Ok("Save Data");
            }
        }
    }
}
