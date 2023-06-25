using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface ISupplierServices : ICrudServices<Supplier>
    {
        Task<Page<Supplier>> GetAsync(SupplierPage pagination, CancellationToken cancellationToken);
    }
}
