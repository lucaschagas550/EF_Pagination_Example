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
            return await _categoryRepository.Create(category).ConfigureAwait(false);
        }

        public async Task Delete(Guid id)
        {
            await _categoryRepository.Delete(id).ConfigureAwait(false);
        }

        public async Task<Page<Category>> Get(CategoryPage pagination)
        {
            return await _categoryRepository.Get(pagination).ConfigureAwait(false);
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await _categoryRepository.GetById(id).ConfigureAwait(false);
        }

        public async Task<Category> Update(Category category)
        {
            return await _categoryRepository.Update(category).ConfigureAwait(false);
        }
    }
}