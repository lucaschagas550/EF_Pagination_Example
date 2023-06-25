using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryProductService _categoryProductService;

        public ProductService(INotifier notifier, IProductRepository productRepository, ICategoryProductService categoryProductRepository) : base(notifier)
        {
            _productRepository = productRepository;
            _categoryProductService = categoryProductRepository;
        }

        public async Task<Page<Product>> GetAsync(ProductPage pagination, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await _productRepository.GetAsync(pagination, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Page<Product>());
            }
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entity = await _productRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

                if (entity is null)
                    Notify("item not found");

                return entity;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Product());
            }
        }

        //Id do CategoryProduct.ProductId Eh setado pelo proprio EF ao salvar no banco, pelo Id do product
        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _productRepository.CreateAsync(product, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, product);
            }
        }

        public async Task<Product> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var product = await _productRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

                if (product is null)
                    return Notify("item not found", new Product());

                var result = await _productRepository.Delete(product, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Product());
            }
        }


        //Id do CategoryProduct.ProductId Eh setado pelo proprio EF ao salvar no banco, pelo Id do product
        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entities = await _categoryProductService.GetAsync(product, cancellationToken).ConfigureAwait(false);
                if (entities.Any())
                    await _categoryProductService.DeleteAsync(entities, cancellationToken).ConfigureAwait(false);

                await _categoryProductService.CreateAsync(product.CategoryProduct, cancellationToken).ConfigureAwait(false); 
                var result = await _productRepository.Update(product, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, product);
            }
        }
    }
}
