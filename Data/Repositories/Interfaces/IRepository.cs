using Azure;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetById(Guid id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(Guid id);
        Task<IEnumerable<TEntity>> Paginate(IQueryable<TEntity> query, Pageable pageable);
    }
}
