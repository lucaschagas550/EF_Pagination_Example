using EF_Pagination_Example.Data.Uow.Interfaces;

namespace EF_Pagination_Example.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private const int Empty = 0;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context) =>
            _context = context;

        public async Task<bool> Commit() =>
            await _context.SaveChangesAsync().ConfigureAwait(false) > Empty;

        public void Dispose()
        { 
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {           
            await _context.DisposeAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }
    }
}