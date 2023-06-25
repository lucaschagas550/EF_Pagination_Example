using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Services
{
    public class SupplierServices : BaseService, ISupplierServices
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierServices(INotifier notifier, ISupplierRepository supplierRepository) : base(notifier)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Page<Supplier>> GetAsync(SupplierPage pagination, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await _supplierRepository.GetAsync(pagination, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Page<Supplier>());
            }
        }

        public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entity = await _supplierRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

                if (entity is null)
                    Notify("item not found");

                return entity;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Supplier());
            }
        }

        public async Task<Supplier> CreateAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _supplierRepository.CreateAsync(supplier, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, supplier);
            }
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _supplierRepository.Update(supplier, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, supplier);
            }
        }

        public async Task<Supplier> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var supplier = await _supplierRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

                if (supplier is null)
                    return Notify("item not found", new Supplier());

                var result = await _supplierRepository.Delete(supplier, cancellationToken).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                return Notify(ex.Message, new Supplier());
            }
        }
    }
}
