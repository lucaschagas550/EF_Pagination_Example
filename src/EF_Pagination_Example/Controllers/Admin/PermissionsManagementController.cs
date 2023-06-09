﻿using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Communication;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Controllers.Admin
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class PermissionsManagementController : MainController
    {
        private readonly IPermissionsManagementService _rolesManagementService;

        public PermissionsManagementController(
            INotifier notifier,
            IPermissionsManagementService rolesManagementService) : base(notifier)
        {
            _rolesManagementService = rolesManagementService;
        }

        [HttpGet("Role")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<PermissionsViewModel>>> Get([FromQuery] RolePage pagination) =>
            CustomResponse(await _rolesManagementService.GetRolesAsync(pagination, CancellationToken.None).ConfigureAwait(false));

        [HttpPost("Role")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> CreateRole([FromBody] string name)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.CreateRoleAsync(name, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPost("AddRoleUser")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> AddRoleUser(UserRoleUpdateViewModel userRoleUpdateViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.AddRoleUserAsync(userRoleUpdateViewModel, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPut("RoleRevoked")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> RoleRevoked(UserRoleRevokedViewModel userRoleRevokedViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.RoleRevokedAsync(userRoleRevokedViewModel, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPost("Claim")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> CreateClaim(ClaimCreateViewModel claimCreateViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.CreateClaimAsync(claimCreateViewModel, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPost("AddClaimUser")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> AddClaimUser(UserClaimUpdateViewModel userClaimUpdateViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.AddClaimUserAsync(userClaimUpdateViewModel, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPut("ClaimRevoked")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> ClaimRevoked(UserClaimRevokedViewModel userClaimRevokedViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.ClaimRevokedAsync(userClaimRevokedViewModel, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpDelete("Claim")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> ClaimDelete(ClaimDeleteViewModel claimDeleteViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.DeleteClaimAsync(claimDeleteViewModel, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpDelete("Role")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> RoleDelete(RoleDeleteViewModel roleDeleteViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.DeleteRoleAsync(roleDeleteViewModel, CancellationToken.None).ConfigureAwait(false));
        }
    }
}
