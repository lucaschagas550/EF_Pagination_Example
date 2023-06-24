using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Interfaces;

public interface ICrudServices<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken);
}