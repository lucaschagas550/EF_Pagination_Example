using System.Security.Claims;

namespace EF_Pagination_Example.Business.Interfaces;

public interface IAspNetUser
{
    string? Name { get; }
    Guid GetUserId();      
    string? GetUserEmail(); 
    string? GetUserToken();
    string? GetUserRefreshToken();
    bool IsAuthenticated();
    bool HasRole(string role);
    IEnumerable<Claim> GetClaims();
    HttpContext? GetHttpContext();
}