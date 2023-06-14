using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.ViewModels;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface IAdminService
    {
        Task<Page<UsersListViewModel>> GetUserAsync(UserPage userPage, CancellationToken cancellationToken);
        Task<UserViewModel> GetByIdAsync(string userId, CancellationToken cancellationToken);
    }
}
