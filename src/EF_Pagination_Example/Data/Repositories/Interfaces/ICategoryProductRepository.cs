using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Data.Repositories.Interfaces
{
    public interface ICategoryProductRepository : IRepository<CategoryProduct>
    {
        Task<List<CategoryProduct>> GetByProductIdAsync(Product product, CancellationToken cancellationToken);
    }
}
