namespace EF_Pagination_Example.Model
{
    public class RefreshToken : Entity
    {
        public string Email { get; set; } = null!;
        public Guid Token { get; set; }
        public DateTime ExpirationDate { get; set; }

        public RefreshToken() { }

        public RefreshToken(string email, DateTime expirationDate)
        {
            Token = Guid.NewGuid();
            Email = email;
            ExpirationDate = expirationDate;
        }
    }
}