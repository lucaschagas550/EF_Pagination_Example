using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface ICategoryService : ICrudServices<Category>
    {
        Task<Page<Category>> Get(CategoryPage pagination, CancellationToken cancellationToken);
    }
}
