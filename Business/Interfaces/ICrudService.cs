using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Interfaces;

public interface ICrudServices<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken);
    Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> Delete(TEntity category, CancellationToken cancellationToken);
}