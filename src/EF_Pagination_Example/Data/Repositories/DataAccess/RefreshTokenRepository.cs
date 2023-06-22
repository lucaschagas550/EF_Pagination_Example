using EF_Pagination_Example.Data.Repositories.Base;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow.Interfaces;
using EF_Pagination_Example.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Data.Repositories.DataAccess
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IUnitOfWork uow, AppDbContext context) : base(uow, context) { }

        public async Task<RefreshToken?> GetRefreshTokenAsync(Guid guid, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await Context()
                    .RefreshTokens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Token.Equals(guid), cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public async Task CreateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Context()
               .RefreshTokens
               .AddAsync(refreshToken, cancellationToken)
               .ConfigureAwait(false);
        }

        public async Task DeleteRefreshTokenAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entities = await Context()
                    .RefreshTokens
                    .AsNoTracking()
                    .Where(r => r.Email.Equals(email))
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                Context()
                    .RefreshTokens
                    .RemoveRange(entities);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }
    }
}
