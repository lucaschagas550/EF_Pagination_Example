using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Communication;
using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : MainController
    {
        private readonly IAdminService _adminService;

        public AdminController(INotifier notifier, IAspNetUser appNetUser, IAdminService adminService) : base(notifier, appNetUser)
        {
            _adminService = adminService;
        }

        [HttpGet("Get/Users")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Page<UserViewModel>>> Get([FromQuery] UserPage pagination) =>
            CustomResponse(await _adminService.GetUserAsync(pagination, CancellationToken.None).ConfigureAwait(false));


    }
}
