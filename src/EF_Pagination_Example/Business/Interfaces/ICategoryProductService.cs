using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface ICategoryProductService
    {
        Task<List<CategoryProduct>> GetAsync(Product product, CancellationToken cancellationToken);
        Task<List<CategoryProduct>> CreateAsync(List<CategoryProduct> entites, CancellationToken cancellationToken);
        Task<List<CategoryProduct>> DeleteAsync(List<CategoryProduct> categoryProducts, CancellationToken cancellationToken);
    }
}
