
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreApi.Apps.AdminApp.DTOs.AccountDtos;
using StoreApi.DATA.Entities;
using StoreApi.Services;
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
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public AccountsController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration,IJwtService jwtService)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._configuration = configuration;
            this._jwtService = jwtService;
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
        /// <summary>
        /// This end point returns token 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/admin/accounts/login
        ///     {
        ///         "userName":"Admin",
        ///         "password":"Admin123"
        ///     }
        /// </remarks>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginDto), 200)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null) return NotFound();

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) 
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            string tokenStr = _jwtService.Generate(user, roles,_configuration);
            return Ok(new {token = tokenStr});
        }
    }
}
