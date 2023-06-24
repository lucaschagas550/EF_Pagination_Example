namespace EF_Pagination_Example.Data.Uow.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task<bool> CommitAsync();
    }
}
