using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Data.Repositories.Base;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow.Interfaces;
using EF_Pagination_Example.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Data.Repositories.DataAccess
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork uow, AppDbContext context) : base(uow, context)
        {
        }

        public async Task<Page<Product>> GetAsync(ProductPage productPage, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queryData = Context().Product.Include(p => p.CategoryProduct).AsQueryable();
                ListApplyWhere(productPage, ref queryData);
                ListApplyOrderBy(productPage, ref queryData);

                var content = await PaginateAsync(queryData, productPage, cancellationToken).ConfigureAwait(false);
                var total = await queryData.CountAsync(cancellationToken).ConfigureAwait(false);

                return new Page<Product>(total, content, productPage);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public override async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await Context()
                    .Product
                    .Include(p => p.CategoryProduct)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        private static void ListApplyWhere(ProductPage productPage, ref IQueryable<Product> queryData)
        {
            if (!string.IsNullOrWhiteSpace(productPage.Search)) queryData = queryData.Where(p => p.Name.ToUpper().Contains(productPage.Search.ToUpper()));
            if (!string.IsNullOrWhiteSpace(productPage.Name)) queryData = queryData.Where(p => p.Name.ToUpper().Equals(productPage.Name.ToUpper()));
            if (!string.IsNullOrWhiteSpace(productPage.Description)) queryData = queryData.Where(p => p.Description.ToUpper().Equals(productPage.Description.ToUpper()));
            if (productPage.LowestPrice > 0) queryData = queryData.Where(p => p.Price <= productPage.LowestPrice);
            if (productPage.HigherPrice > 0) queryData = queryData.Where(p => p.Price <= productPage.HigherPrice);
        }

        private static void ListApplyOrderBy(ProductPage productPage, ref IQueryable<Product> queryData)
        {
            switch (productPage.Sort)
            {
                case nameof(Product.Name):
                    queryData = productPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Name) : queryData.OrderByDescending(o => o.Name);
                    break;
                case nameof(Product.Description):
                    queryData = productPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Description) : queryData.OrderByDescending(o => o.Description);
                    break;
                case nameof(Product.Price):
                    queryData = productPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Price) : queryData.OrderByDescending(o => o.Price);
                    break;
                default:
                    queryData = queryData.OrderBy(o => o.Name);
                    break;
            }
        }
    }
}
