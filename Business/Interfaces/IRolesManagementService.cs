using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using Microsoft.AspNetCore.Identity;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface IRolesManagementService
    {
        Task<Page<IdentityRole>> GetRolesAsync(RolePage rolePage, CancellationToken cancellationToken);
    }
}
