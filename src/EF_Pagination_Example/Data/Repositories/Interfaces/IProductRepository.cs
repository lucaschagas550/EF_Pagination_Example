using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Data.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Page<Product>> GetAsync(ProductPage productPage, CancellationToken cancellationToken);
    }
}
