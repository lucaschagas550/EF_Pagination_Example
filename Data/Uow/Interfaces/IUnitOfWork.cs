namespace EF_Pagination_Example.Data.Uow.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
