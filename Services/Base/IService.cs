using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Services.Base
{
    public interface IService<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetById(Guid id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(Guid id);
    }
}
