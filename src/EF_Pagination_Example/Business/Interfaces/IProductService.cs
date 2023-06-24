using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface IProductService : ICrudServices<Product>
    {
        Task<Page<Product>> GetAsync(ProductPage pagination, CancellationToken cancellationToken);
    }
}
