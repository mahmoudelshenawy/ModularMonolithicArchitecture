using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Module.Users.Core.Entities;
using Shared.Core.Common;

namespace Module.Users.Infrastructure.Persistence.Seeders
{
    public class UserDbContextInitializer
    {
        private readonly ILogger<UserDbContextInitializer> _logger;
        private readonly UserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserDbContextInitializer(UserDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserDbContextInitializer> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task TrySeedAsync()
        {
            // Default roles
            var administratorRole = new IdentityRole(RolesNameConstants.Administrator);
            var userRole = new IdentityRole(RolesNameConstants.User);
            var customerRole = new IdentityRole(RolesNameConstants.Customer);

            var constantsRoleNames = typeof(RolesNameConstants).GetFields(System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.Public).Where(x => x.IsLiteral && !x.IsInitOnly)
                .Select(x => x.GetValue(null))
                .Cast<string>()
                .ToList();
            if (_context.Roles.Count() != constantsRoleNames.Count())
            {
                foreach (var name in constantsRoleNames)
                {
                    if (!_context.Roles.Any(x => x.Name == name))
                        await _roleManager.CreateAsync(new IdentityRole(name));
                }
                //await _roleManager.CreateAsync(administratorRole);
                //await _roleManager.CreateAsync(userRole);
                //await _roleManager.CreateAsync(customerRole);
            }
            var administrator = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Elshenawy",
            };
            if (!_context.Users.Any())
            {
                var result = await _userManager.CreateAsync(administrator, "123456");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(administrator, administratorRole.Name);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
