using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Pagination_Example.Model
{
    public class RefreshToken : Entity
    {
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; } = string.Empty;
        public Guid Token { get; set; }
        public DateTime ExpirationDate { get; set; }

        /* EF One-to-One */
        public AppUser? AppUser { get; set; }

        public RefreshToken() { }

        public RefreshToken(string userId, DateTime expirationDate)
        {
            Token = Guid.NewGuid();
            UserId = userId;
            ExpirationDate = expirationDate;
        }
    }
}