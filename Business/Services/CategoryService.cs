using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Services
{
    public class CategoryService : BaseService, ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(INotifier notifier, ICategoryRepository categoryRepository) : base(notifier)
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
            var entity = await _categoryRepository.GetById(id).ConfigureAwait(false);

            if (entity == null) Notify("item not found");

            return entity;
        }

        public async Task<Category> Update(Category category)
        {
            return await _categoryRepository.Update(category).ConfigureAwait(false);
        }
    }
}