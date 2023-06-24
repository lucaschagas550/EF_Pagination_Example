using EF_Pagination_Example.Data.Repositories.Base;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow.Interfaces;
using EF_Pagination_Example.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Data.Repositories.DataAccess
{
    public class CategoryProductRepository : Repository<CategoryProduct>, ICategoryProductRepository
    {
        public CategoryProductRepository(IUnitOfWork uow, AppDbContext context) : base(uow, context)
        {
        }

        public async Task<List<CategoryProduct>> GetByProductIdAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await Context()
                    .CategoryProduct
                    .AsNoTrackingWithIdentityResolution()
                    .Where(c => c.ProductId.Equals(product.Id))
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                return result;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }
    }
}
