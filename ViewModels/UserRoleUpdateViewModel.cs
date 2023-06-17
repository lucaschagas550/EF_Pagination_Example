namespace EF_Pagination_Example.ViewModels
{
    public class UserRoleUpdateViewModel
    {
        public string UserId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        
        public UserRoleUpdateViewModel() { }

        public UserRoleUpdateViewModel(string userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }
    }
}
