namespace EF_Pagination_Example.ViewModels
{
    public class UserClaimUpdateViewModel : ClaimViewModel
    {
        public string UserId { get; set; } = null!;
        public string RoleId { get; set; } = null!;

        public UserClaimUpdateViewModel() { }

        public UserClaimUpdateViewModel(string userId, string roleId, string type, string value) : base(value, type)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}