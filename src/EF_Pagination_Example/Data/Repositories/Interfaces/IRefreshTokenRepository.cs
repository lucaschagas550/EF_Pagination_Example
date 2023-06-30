using EF_Pagination_Example.Model;
using System.Threading.Tasks;

namespace EF_Pagination_Example.Data.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetRefreshTokenAsync(Guid guid, CancellationToken cancellationToken);
        Task DeleteRefreshTokenAsync(string userId, CancellationToken cancellationToken);
        Task CreateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    }
}
