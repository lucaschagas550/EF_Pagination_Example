namespace EF_Pagination_Example.ViewModels
{
    public class UserClaimRevokedViewModel : ClaimViewModel
    {
        public string UserId { get; set; } = null!;

        public UserClaimRevokedViewModel() { }

        public UserClaimRevokedViewModel(string userId, string type, string value) : base(value, type)
        {
            UserId = userId;
        }
    }
}