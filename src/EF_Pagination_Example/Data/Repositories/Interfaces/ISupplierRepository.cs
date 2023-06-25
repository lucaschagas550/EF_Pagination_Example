using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Data.Repositories.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Page<Supplier>> GetAsync(SupplierPage supplierPage, CancellationToken cancellationToken);
    }
}
