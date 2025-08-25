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

        public DbInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) =>
            (_userManager, _roleManager) = (userManager, roleManager);

        public void Initialize(AuthDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                _roleManager.CreateAsync(new IdentityRole(Roles.Owner)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.User)).GetAwaiter().GetResult();

                AppUser owner = new()
                {
                    UserName = "UserName",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Email = "email@mail.ru"
                };

                _userManager.CreateAsync(owner, "password1").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(owner, Roles.Owner).GetAwaiter().GetResult();

                var claims = _userManager.AddClaimsAsync(owner, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, owner.UserName),
                    new Claim(JwtClaimTypes.GivenName, owner.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, owner.LastName),
                    new Claim(JwtClaimTypes.Email, owner.Email),
                    new Claim(JwtClaimTypes.Role, Roles.Owner)
                }).Result;
            }
        }
    }
}