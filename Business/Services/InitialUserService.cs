using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EF_Pagination_Example.Business.Services
{
    public class InitialUserService : IInitialUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InitialUserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager=roleManager;
        }

        public async Task CreateRole()
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync("Admin").ConfigureAwait(false))
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "Admin";
                    role.NormalizedName = "ADMIN";
                    var roleResult = await _roleManager.CreateAsync(role).ConfigureAwait(false);
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public async Task CreateAdmin()
        {
            try
            {
                if (await _userManager.FindByEmailAsync("admin@admin.com").ConfigureAwait(false) is not null)
                    return;

                var user = new AppUser();
                user.UserName = "admin";
                user.Email = "admin@admin.com";
                user.NormalizedUserName = "ADMIN";
                user.NormalizedEmail = "ADMIN@ADMIN.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                var claims = new List<Claim>()
                {
                    new Claim("Admin","Read"),
                    new Claim("Admin","Insert"),
                    new Claim("Admin","Update"),
                    new Claim("Admin","Delete"),
                };

                var userResult = await _userManager.CreateAsync(user, "Admin@123").ConfigureAwait(false);
                if (userResult.Errors.Any())
                    return;

                var roleResult = await _userManager.AddToRoleAsync(user, "Admin").ConfigureAwait(false);
                if (roleResult.Errors.Any())
                    return;

                var userClaimresult = await _userManager.AddClaimsAsync(user, claims).ConfigureAwait(false);
                if (userClaimresult.Errors.Any())
                    return;

                var role = await _roleManager.FindByNameAsync("Admin").ConfigureAwait(false);
                var roles = await _roleManager.FindByNameAsync("Admin").ConfigureAwait(false);

                if(role == null) 
                    return;

                foreach (var item in claims)
                {
                    var roleClaimResult = await _roleManager.AddClaimAsync(role, item).ConfigureAwait(false);
                    if(roleClaimResult.Errors.Any())
                        return;
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }
    }
}