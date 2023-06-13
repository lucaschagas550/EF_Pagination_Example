using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Business.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private const int LessOne = 1;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(
            INotifier notifier,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager) : base(notifier)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Page<UserViewModel>> GetUserAsync(UserPage userPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var queryData = _userManager.Users.AsQueryable();
            ListApplyWhereUser(userPage, ref queryData);
            ListApplyOrderByUser(userPage, ref queryData);

            var users = await queryData
                .AsNoTracking()
                .Skip((userPage.Page - LessOne) * userPage.Size)
                .Take(userPage.Size)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            
            var total = await queryData
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);

            var usersWithRolesAndClaims = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).ToList();
                var claims = (await _userManager.GetClaimsAsync(user).ConfigureAwait(false)).ToList();
                usersWithRolesAndClaims.Add (new UserViewModel(user.Id, user.UserName ?? "", user.Email ?? "", roles, claims));
            }

            return new Page<UserViewModel>(total, usersWithRolesAndClaims, userPage);
        }

        private static void ListApplyWhereUser(UserPage userPage, ref IQueryable<AppUser> queryData)
        {
            if (!string.IsNullOrWhiteSpace(userPage.Search)) queryData = queryData.Where(c =>  c.Email.ToUpper().Contains(userPage.Search.ToUpper()));
            if (!string.IsNullOrWhiteSpace(userPage.UserName)) queryData = queryData.Where(c => c.UserName.ToUpper().Equals(userPage.UserName.ToUpper()));
            if (!string.IsNullOrWhiteSpace(userPage.Email)) queryData = queryData.Where(o => o.Email.ToUpper().Equals(userPage.Email.ToUpper()));
        }

        private static void ListApplyOrderByUser(UserPage userPage, ref IQueryable<AppUser> queryData)
        {
            switch (userPage.Sort)
            {
                case nameof(AppUser.UserName):
                    queryData = userPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.UserName) : queryData.OrderByDescending(o => o.UserName);
                    break;
                case nameof(AppUser.Email):
                    queryData = userPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Email) : queryData.OrderByDescending(o => o.Email);
                    break;
                default:
                    queryData = queryData.OrderBy(o => o.Email);
                    break;
            }
        }
    }
}
