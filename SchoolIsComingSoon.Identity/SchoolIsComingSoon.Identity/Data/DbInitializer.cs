using Duende.IdentityModel;
using Microsoft.AspNetCore.Identity;
using SchoolIsComingSoon.Identity.Common.Constants;
using SchoolIsComingSoon.Identity.Models;
using System.Security.Claims;

namespace SchoolIsComingSoon.Identity.Data
{
    public class DbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public DbInitializer(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task InitializeAsync(AuthDbContext context)
        {
            var defaultUser = _configuration.GetSection("DefaultAdmin");

            var username = defaultUser["Username"];
            var email = defaultUser["Email"];
            var password = defaultUser["Password"];
            var firstName = defaultUser["FirstName"];
            var lastName = defaultUser["LastName"];

            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
                return;

            await _roleManager.CreateAsync(new IdentityRole(Roles.Owner));
            await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            await _roleManager.CreateAsync(new IdentityRole(Roles.User));

            var owner = new AppUser
            {
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            var createResult = await _userManager.CreateAsync(owner, password);

            if (createResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(owner, Roles.Owner);
                await _userManager.AddClaimsAsync(owner, new[]
                {
                    new Claim(JwtClaimTypes.Name, owner.UserName),
                    new Claim(JwtClaimTypes.GivenName, owner.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, owner.LastName),
                    new Claim(JwtClaimTypes.Email, owner.Email),
                    new Claim(JwtClaimTypes.Role, Roles.Owner)
                });
            }
            else
            {
                throw new InvalidOperationException("Error creating the default user");
            }
        }
    }
}