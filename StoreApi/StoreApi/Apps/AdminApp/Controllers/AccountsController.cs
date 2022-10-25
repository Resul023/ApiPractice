
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreApi.Apps.AdminApp.DTOs.AccountDtos;
using StoreApi.DATA.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreApi.Apps.AdminApp.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountsController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        //public IActionResult Create()
        //{
        //    var roleAdmin = _roleManager.CreateAsync(new IdentityRole("Admin")).Result;
        //    var roleMember = _roleManager.CreateAsync(new IdentityRole("Member")).Result;

        //    var userAdmin = new AppUser { FullName = "Super Admin", UserName = "Admin" };
        //    var userMember = new AppUser { FullName = "Hikmet Abbasli", UserName = "Hikmet" };

        //    var accountAdmin = _userManager.CreateAsync(userAdmin,"Admin123").Result;
        //    var accountMember= _userManager.CreateAsync(userMember, "Member123").Result;

        //    var addRoleToAdmin = _userManager.AddToRoleAsync(userAdmin, "Admin").Result;
        //    var addRoleToMember = _userManager.AddToRoleAsync(userMember, "Member").Result;
        //    return Ok();
        //}
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null) return NotFound();

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) 
                return NotFound();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("FullName",user.FullName),
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());

            string keyStr = "a2b5851a-af6f-47ea-94d3-a7465fa9401b";
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires:DateTime.Now.AddDays(2),
                issuer: "https://localhost:44327/",
                audience: "https://localhost:44327/"
                );
            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new {token = tokenStr});
        }
    }
}
