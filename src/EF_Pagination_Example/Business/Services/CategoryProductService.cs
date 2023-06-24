using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Services
{
    public class CategoryProductService : BaseService, ICategoryProductService
    {
        private readonly ICategoryProductRepository _categoryProductRepository;

        public CategoryProductService(INotifier notifier, ICategoryProductRepository categoryProductRepository) : base(notifier)
        {
            _categoryProductRepository = categoryProductRepository;
        }

        public async Task<List<CategoryProduct>> GetAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await _categoryProductRepository.GetByProductIdAsync(product, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new List<CategoryProduct>());
            }
        }

        public async Task<List<CategoryProduct>> CreateAsync(List<CategoryProduct> categoryProducts, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entites = await _categoryProductRepository.CreateAsync(categoryProducts, cancellationToken).ConfigureAwait(false);

                return entites;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new List<CategoryProduct>());
            }
        }

        public async Task<List<CategoryProduct>> DeleteAsync(List<CategoryProduct> categoryProducts, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entites = await _categoryProductRepository.DeleteRange(categoryProducts, cancellationToken).ConfigureAwait(false);

                return entites;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new List<CategoryProduct>());
            }
        }
    }
}
