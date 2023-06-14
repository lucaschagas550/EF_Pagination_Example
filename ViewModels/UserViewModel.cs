using EF_Pagination_Example.Model;
using System.Security.Claims;

namespace EF_Pagination_Example.ViewModels
{
    public class UserViewModel
    {
        public AppUser AppUser { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();

        public UserViewModel() { }

        public UserViewModel(AppUser appUser, IEnumerable<string> roles, IEnumerable<Claim> claims)
        {
            AppUser = appUser;
            Roles = roles;
            Claims = claims;
        }
    }
}
