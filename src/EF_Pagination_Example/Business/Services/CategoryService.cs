using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(INotifier notifier, ICategoryRepository categoryRepository) : base(notifier)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Create(Category category, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _categoryRepository.Create(category, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Category> Delete(Category category, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _categoryRepository.Delete(category, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Page<Category>> Get(CategoryPage pagination, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _categoryRepository.Get(pagination, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Category?> GetById(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _categoryRepository.GetById(id, cancellationToken).ConfigureAwait(false);

            if (entity == null) Notify("item not found");

            return entity;
        }

        public async Task<Category> Update(Category category, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _categoryRepository.Update(category, cancellationToken).ConfigureAwait(false);
        }
    }
}