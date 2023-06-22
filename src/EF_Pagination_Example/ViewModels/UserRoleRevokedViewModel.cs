namespace EF_Pagination_Example.ViewModels
{
    public class UserRoleRevokedViewModel : UserRoleUpdateViewModel
    {
        public UserRoleRevokedViewModel() { }
        public UserRoleRevokedViewModel(string userId, string roleName ) : base (userId, roleName) { }
    }
}
