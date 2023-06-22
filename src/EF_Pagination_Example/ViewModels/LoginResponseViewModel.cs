namespace EF_Pagination_Example.ViewModels
{
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; } = null!;
        public Guid RefreshToken { get; set; }

        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; } = null!;
    }
}