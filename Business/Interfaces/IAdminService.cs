using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.ViewModels;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface IAdminService
    {
        Task<Page<UserViewModel>> GetUserAsync(UserPage userPage, CancellationToken cancellationToken);
    }
}
