using Microsoft.AspNetCore.Mvc;
using Module.Users.Core.Dtos;
using Module.Users.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Users.Controllers
{
    [ApiController]
    [Route("api/admins/auth")]
    public class AdminAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationRepository _userAuth;
        private readonly IAdminAuthenticationRepository _adminAuth;

        public AdminAuthenticationController(IUserAuthenticationRepository userAuth,
            IAdminAuthenticationRepository adminAuth)
        {
            _userAuth = userAuth;
            _adminAuth = adminAuth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _userAuth.GetTokenAsync(loginDto);
            return Ok(response);
        }

        [HttpPost("add-admin")]
        public async Task<IActionResult> CreateAdmin(RegisterDto registerDto)
        {
            var response = await _adminAuth.AddNewAdmin(registerDto);
            return Ok(response);
        }
    }
}
