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

        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _categoryRepository.CreateAsync(category, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, category);
            }
        }

        public async Task<Category> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var category = await _categoryRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
                if (category is null)
                    return Notify("item not found", new Category());

                var result = await _categoryRepository.Delete(category, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Category());
            }
        }

        public async Task<Page<Category>> GetAsync(CategoryPage pagination, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await _categoryRepository.GetAsync(pagination, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Page<Category>());
            }
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entity = await _categoryRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

                if (entity == null) Notify("item not found");

                return entity;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Category());
            }
        }

        public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _categoryRepository.Update(category, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, category);
            }
        }
    }
}