using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Module.Users.Core.Dtos;
using Module.Users.Core.Entities;
using Module.Users.Core.Interfaces;
using Module.Users.Infrastructure.Persistence;
using Shared.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Users.Infrastructure.Services
{
    public class AdminAuthenticationRepository : UserAuthenticationRepository, IAdminAuthenticationRepository
    {
        private readonly UserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public AdminAuthenticationRepository(UserDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IEmailService emailService,
            IJwtAuthenticationManager jwtAuthenticationManager) :
            base(context, userManager, signinManager, roleManager, configuration, emailService, jwtAuthenticationManager)
        {
        }

        public async Task<RegisterDto> AddNewAdmin(RegisterDto registerDto)
        {
            return await RegisterUser(registerDto);
        }
    }
}
