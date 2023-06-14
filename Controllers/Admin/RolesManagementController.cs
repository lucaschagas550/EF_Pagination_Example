using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Communication;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Controllers.Admin
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class RolesManagementController : MainController
    {
        private readonly IRolesManagementService _rolesManagementService;

        public RolesManagementController(
            INotifier notifier,
            IAspNetUser aspNetUser,
            IRolesManagementService rolesManagementService) : base(notifier, aspNetUser)
        {
            _rolesManagementService = rolesManagementService;
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<IdentityRole>>> Get([FromQuery] RolePage pagination) =>
            CustomResponse(await _rolesManagementService.GetRolesAsync(pagination, CancellationToken.None).ConfigureAwait(false));
    }
}
