using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Model;
using EF_Pagination_Example.Services.Interfaces;

namespace EF_Pagination_Example.Services
{
    public class CategoryService : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Create(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Page<Category>> Get(CategoryPage pagination)
        {
            return await _categoryRepository.Get(pagination).ConfigureAwait(false);
        }

        public async Task<Category> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}