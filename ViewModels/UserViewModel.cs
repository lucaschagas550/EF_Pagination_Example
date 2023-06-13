using System.Security.Claims;

namespace EF_Pagination_Example.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<string> Roles{ get; set; } = new List<string>();
        public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();

        public UserViewModel() { }

        public UserViewModel(string id, string userName, string email, List<string> roles, List<Claim> claims)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Roles = roles;
            Claims = claims;
        }
    }
}
