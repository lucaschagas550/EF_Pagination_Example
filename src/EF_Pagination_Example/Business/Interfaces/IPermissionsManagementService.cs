﻿using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface IPermissionsManagementService
    {
        Task<Page<PermissionsViewModel>> GetRolesAsync(RolePage rolePage, CancellationToken cancellationToken);
        Task<IdentityResult> CreateRoleAsync(string name, CancellationToken cancellationToken);
        Task<IdentityResult> AddRoleUserAsync(UserRoleUpdateViewModel userRoleUpdateViewModel, CancellationToken cancellationToken);
        Task<IdentityResult> RoleRevokedAsync(UserRoleRevokedViewModel userRoleRevokedViewModel, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteRoleAsync(RoleDeleteViewModel roleDeleteViewModel, CancellationToken cancellationToken);
        Task<IdentityResult> CreateClaimAsync(ClaimCreateViewModel claimCreateViewModel, CancellationToken cancellationToken);
        Task<IdentityResult> AddClaimUserAsync(UserClaimUpdateViewModel userClaimUpdateViewModel, CancellationToken cancellationToken);
        Task<IdentityResult> ClaimRevokedAsync(UserClaimRevokedViewModel userClaimRevokedViewModel, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteClaimAsync(ClaimDeleteViewModel claimDeleteViewModel, CancellationToken cancellationToken);
    }
}
