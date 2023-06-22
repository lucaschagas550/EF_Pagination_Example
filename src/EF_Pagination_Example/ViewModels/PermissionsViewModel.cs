using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EF_Pagination_Example.ViewModels
{
    public class PermissionsViewModel
    {
        public IdentityRole Role { get; set; } = new IdentityRole();
        public List<Claim> Claims { get; set; } = new List<Claim>();

        public PermissionsViewModel() { }

        public PermissionsViewModel(IdentityRole role, List<Claim> claims)
        {
            Role = role;
            Claims = claims;
        }
    }
}
