using EF_Pagination_Example.Business.Interfaces;
using System.Security.Claims;

namespace EF_Pagination_Example.Extensions
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor) =>
            _accessor = accessor;

        public string? Name => _accessor.HttpContext?.User.Identity?.Name;

        public Guid GetUserId() =>
            IsAuthenticated() ? Guid.Parse(_accessor.HttpContext?.User.GetUserId() ?? string.Empty) : Guid.Empty;

        public string? GetUserEmail() =>
            IsAuthenticated() ? _accessor.HttpContext?.User.GetUserEmail() : "";

        public string? GetUserToken() =>
            IsAuthenticated() ? _accessor.HttpContext?.User.GetUserToken() : "";

        public string? GetUserRefreshToken() =>
            IsAuthenticated() ? _accessor.HttpContext?.User.GetUserRefreshToken() : "";

        public bool IsAuthenticated() =>
            _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

        public bool HasRole(string role) =>
            _accessor.HttpContext?.User.IsInRole(role) ?? false;

        public IEnumerable<Claim> GetClaims() =>
            _accessor.HttpContext?.User.Claims ?? new List<Claim>();

        public HttpContext? GetHttpContext() =>
            _accessor.HttpContext;
    }
}