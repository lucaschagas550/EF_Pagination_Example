using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Model;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EF_Pagination_Example.Business.Services.Admin
{
    public class PermissionsManagementService : BaseService, IPermissionsManagementService
    {
        private const int LESS_ONE = 1;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public PermissionsManagementService(
            INotifier notifier,
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager) : base(notifier)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<Page<PermissionsViewModel>> GetRolesAsync(RolePage rolePage, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queryData = _roleManager.Roles.AsQueryable();
                ListApplyWhereRole(rolePage, ref queryData);
                ListApplyOrderByRole(rolePage, ref queryData);

                var roles = await queryData
                    .AsNoTracking()
                    .Skip((rolePage.Page - LESS_ONE) * rolePage.Size)
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
            catch (Exception exception)
            {
                return Notify(exception.Message, new Page<PermissionsViewModel>());
            }
        }

        public async Task<IdentityResult> CreateRoleAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (await _roleManager.RoleExistsAsync(name).ConfigureAwait(false))
                    return Notify("Role already exists.", new IdentityResult());

                var role = new IdentityRole
                {
                    Name = name,
                    NormalizedName = name.ToUpper(),
                };

                return await _roleManager.CreateAsync(role).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                return Notify(exception.Message, new IdentityResult());
            }
        }

        public async Task<IdentityResult> CreateClaimAsync(ClaimCreateViewModel claimCreateViewModel, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var newClaim = new Claim(claimCreateViewModel.Type, claimCreateViewModel.Value);

                var role = await _roleManager
                    .Roles
                    .FirstOrDefaultAsync(r => r.Id.Equals(claimCreateViewModel.Role.Id), cancellationToken)
                    .ConfigureAwait(false);

                if (role is null)
                    return Notify("Role not found.", new IdentityResult());

                var claims = (await _roleManager.GetClaimsAsync(role).ConfigureAwait(false)).ToList();

                var result = claims
                    .Where(c => c.Type.Contains(newClaim.Type)
                    && c.Value.Contains(newClaim.Value));

                if (result.Any())
                    return Notify("Claim already registered.", new IdentityResult());

                return await _roleManager.AddClaimAsync(role, newClaim);
            }
            catch (Exception exception)
            {
                return Notify(exception.Message, new IdentityResult());
            }
        }

        public async Task<IdentityResult> AddRoleUserAsync(UserRoleUpdateViewModel userRoleUpdateViewModel, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var user = await _userManager
                    .Users
                    .FirstOrDefaultAsync(u => u.Id.Equals(userRoleUpdateViewModel.UserId), cancellationToken)
                    .ConfigureAwait(false);

                if (user is null)
                    return Notify("User not found.", new IdentityResult());

                if (await _roleManager.RoleExistsAsync(userRoleUpdateViewModel.RoleName).ConfigureAwait(false))
                    return await _userManager.AddToRoleAsync(user, userRoleUpdateViewModel.RoleName).ConfigureAwait(false);

                return Notify("Role does not exist.", new IdentityResult());
            }
            catch (Exception exception)
            {
                return Notify(exception.Message, new IdentityResult());
            }
        }

        public async Task<IdentityResult> AddClaimUserAsync(UserClaimUpdateViewModel userClaimUpdateViewModel, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var user = await _userManager
                    .Users
                    .FirstOrDefaultAsync(u => u.Id.Equals(userClaimUpdateViewModel.UserId), cancellationToken)
                    .ConfigureAwait(false);

                if (user is null)
                    return Notify("User not found.", new IdentityResult());

                var isMemberRole = await _userManager.IsInRoleAsync(user, userClaimUpdateViewModel.RoleName).ConfigureAwait(false);
                if (isMemberRole == false)
                    return Notify("User does not belong to this role.", new IdentityResult());

                var claim = new Claim(userClaimUpdateViewModel.Type, userClaimUpdateViewModel.Value);
                var hasClaim = (await _userManager.GetClaimsAsync(user)).Any(c => c.Type.Equals(claim.Type) && c.Value.Equals(claim.Value));
                if (hasClaim)
                    return Notify("User already has this claim for this role.", new IdentityResult());

                return await _userManager.AddClaimAsync(user, claim);
            }
            catch (Exception exception)
            {
                return Notify(exception.Message, new IdentityResult());
            }
        }

        public async Task<IdentityResult> ClaimRevokedAsync(UserClaimRevokedViewModel userClaimRevokedViewModel, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var user = await _userManager
                    .Users
                    .FirstOrDefaultAsync(u => u.Id.Equals(userClaimRevokedViewModel.UserId), cancellationToken)
                    .ConfigureAwait(false);

                if (user is null)
                    return Notify("User not found.", new IdentityResult());

                var userClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);
                var claim = new Claim(userClaimRevokedViewModel.Type, userClaimRevokedViewModel.Value);

                if (userClaims.Any(c => c.Type.Equals(claim.Type) && c.Value.Equals(claim.Value)))
                    return await _userManager.RemoveClaimAsync(user, claim).ConfigureAwait(false);

                return Notify("User does not have the claim.", new IdentityResult());
            }
            catch (Exception exception)
            {
                return Notify(exception.Message, new IdentityResult());
            }
        }

        public async Task<IdentityResult> RoleRevokedAsync(UserRoleRevokedViewModel userRoleRevokedViewModel, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var user = await _userManager
                    .Users
                    .FirstOrDefaultAsync(u => u.Id.Equals(userRoleRevokedViewModel.UserId), cancellationToken)
                    .ConfigureAwait(false);

                if (user is null)
                    return Notify("User not found.", new IdentityResult());

                var roleResult = await _userManager.RemoveFromRoleAsync(user, userRoleRevokedViewModel.RoleName).ConfigureAwait(false);
                if (roleResult.Errors.Any())
                {
                    foreach (var error in roleResult.Errors)
                        Notify(error.Description);

                    return new IdentityResult();
                }

                var claimsUser = await _userManager.GetClaimsAsync(user);
                var revokedClaim = claimsUser.Where(c => c.Type.Equals(userRoleRevokedViewModel.RoleName));
                return await _userManager.RemoveClaimsAsync(user, revokedClaim);
            }
            catch (Exception exception)
            {
                return Notify(exception.Message, new IdentityResult());
            }
        }

        public async Task<IdentityResult> DeleteClaimAsync(ClaimDeleteViewModel claimDeleteViewModel, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id.Equals(claimDeleteViewModel.RoleId), cancellationToken).ConfigureAwait(false);
                if (role is null)
                    return Notify("Role not found.", new IdentityResult());

                var claim = new Claim(claimDeleteViewModel.Type, claimDeleteViewModel.Value);
                var users = await _userManager.Users.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);

                foreach (var user in users)
                {
                    var userClaim = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);
                    var removeClaim = userClaim.FirstOrDefault(c => c.Type.Equals(claim.Type) && c.Value.Equals(claim.Value));

                    if (removeClaim is not null)
                        await _userManager.RemoveClaimAsync(user, removeClaim).ConfigureAwait(false);
                }

                return await _roleManager.RemoveClaimAsync(role, claim).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                return Notify(exception.Message, new IdentityResult());
            }
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