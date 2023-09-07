using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Data.Repositories.Base;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow.Interfaces;
using EF_Pagination_Example.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Data.Repositories.DataAccess
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(IUnitOfWork uow, AppDbContext context) : base(uow, context)
        {
        }

        public async Task<Page<Supplier>> GetAsync(SupplierPage supplierPage, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queryData = Context()
                    .Supplier
                    .Include(p => p.Products)
                    .Include(a => a.Address)
                        .ThenInclude(a => a.Audit.Created)
                    .AsQueryable();
                ListApplyWhere(supplierPage, ref queryData);
                ListApplyOrderBy(supplierPage, ref queryData);

                var content = await PaginateAsync(queryData, supplierPage, cancellationToken).ConfigureAwait(false);
                var total = await queryData.CountAsync(cancellationToken).ConfigureAwait(false);

                return new Page<Supplier>(total, content, supplierPage);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public override async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {//Criar um endpoint q retorne uma dto, com produto e address e apenas o nome do usuario e data que criou
                //return await Context()
                //.Supplier
                //.Include(p => p.Products)
                //.Include(a => a.Address)
                //    .ThenInclude(a => a.Audit.Created)
                //.AsNoTracking()
                //.Select(o => new Supplier()
                //{
                //    Id = o.Id,
                //})
                //.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken)
                //.ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();

                return await Context()
                    .Supplier
                    .Include(p => p.Products)
                    .Include(a => a.Address)
                        .ThenInclude(a => a.Audit.Created)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        private static void ListApplyWhere(SupplierPage supplierPage, ref IQueryable<Supplier> queryData)
        {
            if (!string.IsNullOrWhiteSpace(supplierPage.Search)) queryData = queryData.Where(p => p.Name.ToUpper().Contains(supplierPage.Search.ToUpper()));
            if (!string.IsNullOrWhiteSpace(supplierPage.Name)) queryData = queryData.Where(p => p.Name.ToUpper().Equals(supplierPage.Name.ToUpper()));
            if (!string.IsNullOrWhiteSpace(supplierPage.Document)) queryData = queryData.Where(p => p.Document.ToUpper().Equals(supplierPage.Document.ToUpper()));
            if ((int)supplierPage.TypeSupplier > 0) queryData = queryData.Where(p => p.TypeSupplier.Equals(supplierPage.TypeSupplier));
            if ((int)supplierPage.Active > 0) queryData = queryData.Where(p => p.Active.Equals(supplierPage.Active));
        }

        private static void ListApplyOrderBy(SupplierPage supplierPage, ref IQueryable<Supplier> queryData)
        {
            switch (supplierPage.Sort)
            {
                case nameof(Supplier.Name):
                    queryData = supplierPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Name) : queryData.OrderByDescending(o => o.Name);
                    break;
                case nameof(Supplier.Document):
                    queryData = supplierPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Document) : queryData.OrderByDescending(o => o.Document);
                    break;
                case nameof(Supplier.TypeSupplier):
                    queryData = supplierPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.TypeSupplier) : queryData.OrderByDescending(o => o.TypeSupplier);
                    break;
                case nameof(Supplier.Active):
                    queryData = supplierPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Active) : queryData.OrderByDescending(o => o.Active);
                    break;
                default:
                    queryData = queryData.OrderBy(o => o.Name);
                    break;
            }
        }
    }
}