using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Business.Services.Admin
{
    public class RolesManagementService : BaseService, IRolesManagementService
    {
        private const int LessOne = 1;

        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesManagementService(
            INotifier notifier,
            RoleManager<IdentityRole> roleManager) : base(notifier)
        {
            _roleManager = roleManager;
        }

        public async Task<Page<PermissionsViewModel>> GetRolesAsync(RolePage rolePage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var queryData = _roleManager.Roles.AsQueryable();
            ListApplyWhereRole(rolePage, ref queryData);
            ListApplyOrderByRole(rolePage, ref queryData);

            var roles = await queryData
                .AsNoTracking()
                .Skip((rolePage.Page - LessOne) * rolePage.Size)
                .Take(rolePage.Size)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            var total = await queryData
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);

            var permissionsList = new List<PermissionsViewModel>();
            foreach (var role in roles)
            {
                var claims = (await _roleManager.GetClaimsAsync(role).ConfigureAwait(false)).ToList();
                permissionsList.Add(new PermissionsViewModel(role, claims));
            }

            return new Page<PermissionsViewModel>(total, permissionsList, rolePage);
        }

        private static void ListApplyWhereRole(RolePage rolePage, ref IQueryable<IdentityRole> queryData)
        {
            if (!string.IsNullOrWhiteSpace(rolePage.Search)) queryData = queryData.Where(c => c.Name.ToUpper().Contains(rolePage.Search.ToUpper()));
            if (!string.IsNullOrWhiteSpace(rolePage.Name)) queryData = queryData.Where(c => c.Name.ToUpper().Equals(rolePage.Name.ToUpper()));
        }

        private static void ListApplyOrderByRole(RolePage rolePage, ref IQueryable<IdentityRole> queryData)
        {
            switch (rolePage.Sort)
            {
                case nameof(IdentityRole.Name):
                    queryData = rolePage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Name) : queryData.OrderByDescending(o => o.Name);
                    break;
                default:
                    queryData = queryData.OrderBy(o => o.Name);
                    break;
            }
        }
    }
}