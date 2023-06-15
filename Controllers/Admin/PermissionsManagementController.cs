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
            IAspNetUser aspNetUser,
            IPermissionsManagementService rolesManagementService) : base(notifier, aspNetUser)
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

        [HttpPost("Claim")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> CreateClaim(ClaimCreateViewModel claimCreateViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _rolesManagementService.CreateClaimAsync(claimCreateViewModel, CancellationToken.None).ConfigureAwait(false));
        }
    }
}