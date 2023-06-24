using Azure;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> PaginateAsync(IQueryable<TEntity> query, Pageable pageable, CancellationToken cancellation);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellation);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellation);
        Task<List<TEntity>> CreateAsync(List<TEntity> entity, CancellationToken cancellation);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellation);
        Task<List<TEntity>> Update(List<TEntity> entity, CancellationToken cancellation);
        Task<TEntity> Delete(TEntity entity, CancellationToken cancellation);
        Task<List<TEntity>> DeleteRange(List<TEntity> entities, CancellationToken cancellation);
        Task CommitAsync();
    }
}
