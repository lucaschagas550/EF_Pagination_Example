using Azure;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Paginate(IQueryable<TEntity> query, Pageable pageable, CancellationToken cancellation);
        Task<TEntity?> GetById(Guid id, CancellationToken cancellation);
        Task<TEntity> Create(TEntity entity, CancellationToken cancellation);
        Task<IEnumerable<TEntity>> Create(ICollection<TEntity> entity, CancellationToken cancellation);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellation);
        Task<IEnumerable<TEntity>> Update(ICollection<TEntity> entity, CancellationToken cancellation);
        Task<TEntity> Delete(TEntity entity, CancellationToken cancellation);
        Task<IEnumerable<TEntity>> Delete(ICollection<TEntity> entities, CancellationToken cancellation);
        Task Commit();
    }
}
