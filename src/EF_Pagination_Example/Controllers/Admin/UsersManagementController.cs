using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Communication;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Controllers.Admin
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersManagementController : MainController
    {
        private readonly IAdminService _adminService;

        public UsersManagementController(
            INotifier notifier,
            IAspNetUser aspNetUser,
            IAdminService adminService) : base(notifier, aspNetUser)
        {
            _adminService = adminService;
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<UsersListViewModel>>> Get([FromQuery] UserPage pagination) =>
            CustomResponse(await _adminService.GetUserAsync(pagination, CancellationToken.None).ConfigureAwait(false));

        [HttpGet("GetById/{id:guid}")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserViewModel>> GetById(Guid id) =>
            CustomResponse(await _adminService.GetByIdAsync(id.ToString(), CancellationToken.None).ConfigureAwait(false));

    }
}