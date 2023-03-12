using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;
using EF_Pagination_Example.Services.Base;

namespace EF_Pagination_Example.Services.Interfaces
{
    public interface ICategoryServices : IService<Category>
    {
        Task<Page<Category>> Get(CategoryPage pagination);
    }
}
