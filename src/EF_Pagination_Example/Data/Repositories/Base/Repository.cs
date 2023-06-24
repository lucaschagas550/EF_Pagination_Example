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

        protected readonly IUnitOfWork _uow;
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected Repository(IUnitOfWork uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected AppDbContext Context() => _context;

        public virtual async Task<List<TEntity>> PaginateAsync(IQueryable<TEntity> query, Pageable pageable, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await query
                                .AsNoTrackingWithIdentityResolution()
                                .Skip((pageable.Page - LESS_ONE) * pageable.Size)
                                .Take(pageable.Size)
                                .ToListAsync(cancellationToken)
                                .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
                await _uow.CommitAsync().ConfigureAwait(false);
                return result.Entity;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public virtual async Task<List<TEntity>> CreateAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _dbSet.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
                await _uow.CommitAsync().ConfigureAwait(false);
                return entities;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = _dbSet.Update(entity);
                await _uow.CommitAsync().ConfigureAwait(false);
                return result.Entity;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public virtual async Task<List<TEntity>> Update(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                _dbSet.UpdateRange(entities);
                await _uow.CommitAsync().ConfigureAwait(false);
                return entities;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public virtual async Task<TEntity> Delete(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = _dbSet.Remove(entity);
                await _uow.CommitAsync().ConfigureAwait(false);
                return result.Entity;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public virtual async Task<List<TEntity>> DeleteRange(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                _dbSet.RemoveRange(entities);
                await _uow.CommitAsync().ConfigureAwait(false);
                return entities;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        public virtual async Task CommitAsync()
        {
            try
            {
                await _uow.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }
    }
}