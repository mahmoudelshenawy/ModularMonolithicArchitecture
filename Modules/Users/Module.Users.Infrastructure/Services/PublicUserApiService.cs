using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.Users.Core.Entities;
using Module.Users.Infrastructure.Persistence;
using Module.Users.Shared.Dtos;
using Module.Users.Shared.UserApiInterfaces;
using Shared.Models.Models;

namespace Module.Users.Infrastructure.Services
{
    public class PublicUserApiService : IUserPublicApi
    {
        private readonly UserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PublicUserApiService(UserDbContext userDbContext, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = userDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> EmailIsUnique(string email)
        {
            return await _userManager.FindByEmailAsync(email) == null;  //exits ==> true
        }

        public async Task<IBaseUser> GetUserDetails(string userId)
        {
            var userDetails = new IBaseUser(userId, null, null, new());
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                var refreshToken = user.RefreshToken ?? await _context.RefreshTokens.IgnoreAutoIncludes().FirstOrDefaultAsync(x => x.UserId == userId);
                var relationsDict = new Dictionary<string, List<object>>();
                relationsDict.Add("refreshTokens", new List<object> { refreshToken });
                var roles = await _userManager.GetRolesAsync(user);
                userDetails = new IBaseUser(userId, user.Email, user.UserName, roles.ToList() , relationsDict);
            }

            return userDetails;
        }

        public async Task<string> GetUserEmailById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
                return user.Email!;

            return string.Empty;
        }

        public async Task<Result<string>> RegisterNewUser(string email, string name , string phone, string password, string[] roles)
        {
            try
            {
                if (await EmailIsUnique(email) == false)
                    return Result.Failure<string>(new Error("Email", "email is not unique"));

                var user = new ApplicationUser
                {
                    Email = email,
                    FirstName = name,
                    UserName = email,
                    Status = "Active",
                    PhoneNumber = phone
                };
                IdentityResult identityResult = await _userManager.CreateAsync(user, password);
                if (identityResult.Succeeded)
                {
                    bool rolesAdded;
                    foreach (var role in roles)
                    {
                        if (await _roleManager.Roles.AnyAsync(r => r.Name == role))
                        {
                            IdentityResult identityRole = await _userManager.AddToRoleAsync(user, role);
                            if (identityResult.Succeeded)
                            {
                                rolesAdded = true;
                            }
                        }
                    }
                    return Result.Success<string>(user.Id);
                }
                return Result.Failure<string>(new Error("Register.Failed", "register use had failed"));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<bool> UpdateUserCredentials(string userId, string email, string password, string? phone)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if(user.Email != email || string.IsNullOrEmpty(password))
                {
                    var newEmailIsUnique = await EmailIsUnique(email);
                    if(newEmailIsUnique == true) //Not Taken yet
                    {
                        user.Email = email;
                    }
                   
                    var passwordIsTheSame = await _userManager.CheckPasswordAsync(user, password);
                    if (!passwordIsTheSame)
                    {
                        var PasswordHasher = _userManager.PasswordHasher;
                        var passwordHashed = PasswordHasher.HashPassword(user, password);

                        user.PasswordHash = passwordHashed;
                    }
                    
                }
                user.UserName = email;
                user.PhoneNumber = phone;
                IdentityResult userUpdatedResult = await _userManager.UpdateAsync(user);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateUserRoles(string userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            foreach (var role in roles)
            {
                if (await _roleManager.Roles.AnyAsync(r => r.Name == role))
                {
                    IdentityResult identityRole = await _userManager.AddToRoleAsync(user, role);
                    if (!identityRole.Succeeded)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
    }
}
