namespace EF_Pagination_Example.ViewModels
{
    public class UserTokenViewModel
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<ClaimViewModel> Claims { get; set; } = null!;
    }
}