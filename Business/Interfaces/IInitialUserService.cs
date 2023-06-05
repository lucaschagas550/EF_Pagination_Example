namespace EF_Pagination_Example.Business.Interfaces
{
    public interface IInitialUserService
    {
        Task CreateRole();
        Task CreateAdmin();
    }
}
