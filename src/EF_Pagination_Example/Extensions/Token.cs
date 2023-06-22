namespace EF_Pagination_Example.Extensions
{
    public class Token
    {
        public string Secret { get; set; } = null!;
        public int ExpirationHours { get; set; }
        public int RefreshTokenExpirationHours { get; set; }
        public string Issuer { get; set; } = null!;
        public string ValidOn { get; set; } = null!;
    }
}