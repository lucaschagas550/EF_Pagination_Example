using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow.Interfaces;
using EF_Pagination_Example.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Data.Repositories.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private const int LESS_ONE = 1;

        private readonly IUnitOfWork _uow;
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(IUnitOfWork uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            try
            {
                return await dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id)).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                var result = await dbSet.AddAsync(entity).ConfigureAwait(false);
                await _uow.Commit().ConfigureAwait(false);
                return result.Entity;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var entity = dbSet.FirstOrDefault(e => e.Id.Equals(id));

                if (entity != null)
                {
                    var result = dbSet.Remove(entity);
                    await _uow.Commit().ConfigureAwait(false);
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                var result = dbSet.Update(entity);
                await _uow.Commit().ConfigureAwait(false);
                return result.Entity;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public async Task<IEnumerable<TEntity>> Paginate(IQueryable<TEntity> query, Pageable pageable)
        {
            try
            {
                return await query.AsNoTrackingWithIdentityResolution()
                   .Skip((pageable.Page - LESS_ONE) * pageable.Size)
                   .Take(pageable.Size)
                   .ToListAsync()
                   .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }
    }
}
