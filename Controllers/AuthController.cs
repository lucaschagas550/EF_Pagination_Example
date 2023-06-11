using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Communication;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Controllers
{
    [Route("[controller]")]
    public class AuthController : MainController
    {
        private readonly IAuthService _authService;

        public AuthController(INotifier notifier, IAspNetUser aspNetUser,IAuthService authService) : base(notifier, aspNetUser) =>
            _authService = authService;

        [HttpPost("Login")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseViewModel>> Login(LoginRequestViewModel loginRequest)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return CustomResponse(await _authService.LoginAsync(loginRequest, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpPost("Refresh-Token")]
        [ProducesResponseType(typeof(ResponseSuccess), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseFailure), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                NotifyError("Invalid Refresh Token.");
                return CustomResponse();
            }

            var token = await _authService.GetRefreshTokenAsync(Guid.Parse(refreshToken), CancellationToken.None).ConfigureAwait(false);

            if (token is null)
            {
                NotifyError("Expired Refresh Token.");
                return CustomResponse();
            }

            return CustomResponse(await _authService.GenerateJwtAsync(token.Email, CancellationToken.None).ConfigureAwait(false));
        }
    }
}