using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Users.Core.Dtos;
using Module.Users.Core.Interfaces;
using System.Security.Claims;

namespace Module.Users.Controllers
{
    [ApiController]
    [Route("api/users/auth")]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationRepository _userAuth;

        public UserAuthenticationController(IUserAuthenticationRepository userAuth)
        {
            _userAuth = userAuth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _userAuth.RegisterUser(registerDto);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _userAuth.GetTokenAsync(loginDto);
            return Ok(response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken.Token) || string.IsNullOrEmpty(refreshToken.RefreshToken))
            {
                return BadRequest();
            }
            var response = await _userAuth.GenerateRegreshToken(refreshToken);
            return Ok(response);
        }
        [HttpPost("revoke-token")]
        [Authorize]
        public async Task<IActionResult> RevokeToken()
        {
            var response = await _userAuth.RevokeToken(User.FindFirstValue("UserId"));

            return response ? Ok() : BadRequest();
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string uid, [FromQuery] string token)
        {
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(uid))
            {
                token = token.Replace(' ', '+');
                var response = await _userAuth.ConfirmUserEmail(uid, token);
                return Ok(response);
            }
            return NoContent();
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userAuth.SendResetPasswordConfirmationToUser(forgotPassword);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!string.IsNullOrEmpty(resetPasswordDto.UserId) && !string.IsNullOrEmpty(resetPasswordDto.Token))
            {
                resetPasswordDto.Token = resetPasswordDto.Token.Replace(' ', '+');
                var response = await _userAuth.ResetPassword(resetPasswordDto);
                return Ok(response);
            }
            return NoContent();
        }
    }
}
