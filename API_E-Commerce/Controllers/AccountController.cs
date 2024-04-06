using API_E_Commerce.DTO;
using API_E_Commerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using API_E_Commerce.Repository;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authentication;

namespace API_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        ICRUD_Repository<IdentityRole> r;
        public AccountController(UserManager<ApplicationUser> user, IConfiguration configuration,
            ICRUD_Repository<IdentityRole> R, RoleManager<IdentityRole> roleManager )
        {
            this._userManager = user;
            this.configuration = configuration;
            this.r = R;
            this._roleManager = roleManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO RDTO, [FromHeader] string NameRole)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = RDTO.UserName;
                applicationUser.Email = RDTO.Email;
                applicationUser.PhoneNumber = RDTO.Phone;
                applicationUser.ProfilePictureUrl = RDTO.Image;
                IdentityResult result;
                if (NameRole == "Admin")
                {
                    result = await _userManager.CreateAsync(applicationUser, RDTO.Password);
                    if (result.Succeeded)
                    {
                        var roleExist = await _roleManager.RoleExistsAsync("Admin");
                        if (!roleExist)
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        }
                        var RoleResult = await _userManager.AddToRoleAsync(applicationUser, "Admin");
                        if ( RoleResult.Succeeded)
                            return Ok("Add Succeeded");
                        else
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                                BadRequest(ModelState);
                            }
                    }
                    else
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                            BadRequest(ModelState);
                        }
                }
                else
                {
                    result = await _userManager.CreateAsync(applicationUser, RDTO.Password);
                    if (result.Succeeded)
                        return Ok("Add Succeeded");
                    else
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                            BadRequest(ModelState);
                        }
                }
            }
            else
            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //check
            ApplicationUser UserModel = await _userManager.FindByEmailAsync(login.Email);
            if (UserModel != null)
            {
                if (await _userManager.CheckPasswordAsync(UserModel, login.Password) == true)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("test", "1"));
                    claims.Add(new Claim(ClaimTypes.Email, UserModel.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, UserModel.Id));
                    var roles = await _userManager.GetRolesAsync(UserModel);
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                    //jti  
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    //------------------------------(: Token :)---------------------------------//
                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
                    var Mytoken =
                        new JwtSecurityToken
                        (
                            audience: configuration["JWT:ValidAudience"],
                            issuer: configuration["Jwt:ValidIssue"],
                            //expires: DateTime.MaxValue,
                            expires: DateTime.UtcNow.AddHours(1),
                            claims: claims, 
                            signingCredentials:
                                new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(Mytoken),
                        Exception = Mytoken.ValidTo
                    });
                }
                else
                    BadRequest("password is vaild");
            }
            return Unauthorized();
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }

    }
}
