using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module.Users.Core.Dtos;
using Module.Users.Core.Entities;
using Module.Users.Core.Interfaces;
using Module.Users.Infrastructure.Persistence;
using Shared.Core.Interfaces;
using Shared.Models.Models;
using System.Security.Claims;

namespace Module.Users.Infrastructure.Services
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly UserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public UserAuthenticationRepository(UserDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IEmailService emailService,
            IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public async Task<RegisterDto> ConfirmUserEmail(string userId, string token)
        {
            var registeredDto = new RegisterDto();


            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                registeredDto.FirstName = user.FirstName;
                registeredDto.LastName = user.LastName;
                registeredDto.Email = user.Email;
                registeredDto.Phone = user.PhoneNumber;
                var response = await _userManager.ConfirmEmailAsync(user, token);
                if (response.Succeeded)
                {
                    registeredDto.IsRegistered = true;
                }
                else
                {
                    registeredDto.Messages = response.Errors.Select(x => x.Description).ToList();
                }
            }
            return registeredDto;
        }
        public async Task<ForgotPasswordDto> SendResetPasswordConfirmationToUser(ForgotPasswordDto forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await SendResetPasswordConfirmation(user, token);
                forgotPassword.EmailSent = true;
            }
            return forgotPassword;
        }
        public async Task<RegisterDto> RegisterUser(RegisterDto registerDto)
        {
            try
            {
                _context.Database.BeginTransaction();
                var emailExists = await _userManager.FindByEmailAsync(registerDto.Email);
                if (emailExists != null)
                    registerDto.Messages.Add("Email Is Already taken");

                if (registerDto.Password != registerDto.PasswordConfirmation)
                    registerDto.Messages.Add("passowrd doesnot match");

                var user = new ApplicationUser
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.Phone,
                    UserName = registerDto.Email
                };
                var response = await _userManager.CreateAsync(user, registerDto.Password);

                if (response.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await SendConfirmationEmail(user, token);
                    registerDto.IsRegistered = true;
                    if (registerDto.Roles.Count == 0)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        foreach (var item in registerDto.Roles)
                        {
                            if (_roleManager.Roles.Any(x => x.Name == item))
                                await _userManager.AddToRoleAsync(user, item);
                        }
                    }

                }
                _context.Database.CommitTransaction();
                return registerDto;
            }
            catch (Exception e)
            {
                _context.Database.RollbackTransaction();

                registerDto.Messages.Add(e.Message);
                return registerDto;
            }

        }

        private async Task SendConfirmationEmail(ApplicationUser user, string token)
        {
            var appDomain = _configuration.GetSection("Application:AppDomain").Value;
            var EmailConfirmation = _configuration.GetSection("Application:EmailConfirmation").Value;

            var userEmailOptions = new UserEmailOptions();
            userEmailOptions.ToEmails.Add(user.Email);
            userEmailOptions.Subject = "Confirm You Email";
            userEmailOptions.PlaceHolders = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string> ("{{User}}" , user.FirstName + " " + user.LastName),
                new KeyValuePair<string, string> ("{{Link}}" , string.Format(appDomain+ EmailConfirmation , user.Id , token)),
            };

            await _emailService.SendConfirmationEmail(userEmailOptions);
        }
        private async Task SendResetPasswordConfirmation(ApplicationUser user, string token)
        {
            var appDomain = _configuration.GetSection("Application:AppDomain").Value;
            var ForgotPassword = _configuration.GetSection("Application:ForgotPassword").Value;

            var userEmailOptions = new UserEmailOptions();
            userEmailOptions.ToEmails.Add(user.Email);
            userEmailOptions.Subject = "Reset Your Password";
            userEmailOptions.PlaceHolders = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string> ("{{User}}" , user.FirstName + " " + user.LastName),
                new KeyValuePair<string, string> ("{{Link}}" , string.Format(appDomain+ ForgotPassword , user.Id , token)),
            };

            await _emailService.SendResetPasswordConfirmation(userEmailOptions);
        }

        public async Task<ResetPasswordDto> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId);
            if (user != null)
            {
                var response = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
                if (response.Succeeded)
                {
                    resetPasswordDto.IsReset = true;
                }
                else
                {
                    resetPasswordDto.Messages = response.Errors.Select(x => x.Description).ToList();
                }
            }
            return resetPasswordDto;
        }

        public async Task<AuthModelDto> GetTokenAsync(LoginDto model)
        {
            var authModel = new AuthModelDto();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authModel.Message = "Invalid Credentials";
                return authModel;
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Invalid Credentials";
                return authModel;
            }

            var (token, expiredIn) = await _jwtAuthenticationManager.CreateJwtToken(user);
            var refreshToken = _jwtAuthenticationManager.GenerateRefreshToken();

            var userRefreshToken = await _context.RefreshTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (userRefreshToken == null)
            {
                var newRefreshToken = new RefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    ExpiryDate = expiredIn
                };
                _context.RefreshTokens.Add(newRefreshToken);
            }
            else
            {
                userRefreshToken.Token = refreshToken;
                userRefreshToken.ExpiryDate = DateTime.Now.AddDays(7);
                _context.Update(userRefreshToken);
            }
            await _context.SaveChangesAsync();
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.Token = token;
            authModel.RefreshToken = refreshToken;
            authModel.ExpiresOn = expiredIn;
            authModel.RefreshTokenExpiration = DateTime.Now.AddDays(7);
            authModel.IsAuthenticated = true;
            authModel.Roles = roles.ToList();
            return authModel;

        }
        public async Task<AuthModelDto> GenerateRegreshToken(RefreshTokenDto refreshTokenDto)
        {
            var authModel = new AuthModelDto();
            string accessToken = refreshTokenDto.Token;
            string refreshToken = refreshTokenDto.RefreshToken;
            var principal = _jwtAuthenticationManager.GetPrincipalFromExpiredToken(accessToken);
            var user = await _userManager.Users.Include(u => u.RefreshToken).FirstOrDefaultAsync(u => u.Id == principal.FindFirstValue("UserId"));
            if (user == null || user.RefreshToken?.Token != refreshToken || user.RefreshToken?.ExpiryDate <= DateTime.Now)
            {
                authModel.Message = $"Invalid Client Request";

                return authModel;
            }
            var (token, expiryDate) = await _jwtAuthenticationManager.CreateJwtToken(user);
            var newRefreshToken = _jwtAuthenticationManager.GenerateRefreshToken();

            user.RefreshToken.Token = newRefreshToken;
            user.RefreshToken.ExpiryDate = DateTime.Now.AddDays(7);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            authModel.Email = user.Email;
            authModel.Token = token;
            authModel.RefreshToken = newRefreshToken;
            authModel.ExpiresOn = expiryDate;
            authModel.RefreshTokenExpiration = DateTime.Now.AddDays(7);
            authModel.IsAuthenticated = true;
            authModel.Username = user.UserName;
            return authModel;
        }

        public async Task<bool> RevokeToken(string userId)
        {
            var user = await _context.Users.Include(x => x.RefreshToken).FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return false;

            var RefreshToken = await _context.RefreshTokens.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (RefreshToken != null)
            {
                RefreshToken.Token = null;
                RefreshToken.ExpiryDate = DateTime.Now.AddDays(7);
                _context.Update(RefreshToken);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
