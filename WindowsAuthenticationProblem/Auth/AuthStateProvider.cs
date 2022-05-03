using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace WindowsAuthenticationProblem.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal? user = _httpContextAccessor?.HttpContext?.User;

            var authuser = user is not null && IsUserInMembersList(user) ? user : new ClaimsPrincipal(new ClaimsIdentity());

            return new AuthenticationState(authuser);
        }

        public bool IsUserInMembersList(ClaimsPrincipal user)
        {

            var username = user?.Identity?.Name ?? string.Empty;


            return (new List<string>()
            {
                "JT",
                "TD"
            }).Any(s => s == username);

        }
    }
}
