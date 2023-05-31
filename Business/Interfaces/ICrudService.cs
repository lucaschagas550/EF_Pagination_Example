using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Business.Interfaces;

public interface ICrudServices<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetById(Guid id);
    Task<TEntity> Create(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Delete(TEntity category);
}