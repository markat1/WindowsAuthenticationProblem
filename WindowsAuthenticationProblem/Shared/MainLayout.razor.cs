using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace WindowsAuthenticationProblem.Shared
{
    public partial class MainLayout
    {
        [CascadingParameter] Task<AuthenticationState> authenticationStateTask { get; set; }
        private bool IsLoggedIn { get; set; } = false;
        private IIdentity? User { get; set; } = null;

        protected async override Task OnInitializedAsync()
        {
            IsLoggedIn = await IsUserLoggedIn();
            User = await GetAuthenticatedUserAsync();
        }

        private string GetUserShortName(string? userName)
        {
            var name = !string.IsNullOrEmpty(userName) ? userName : "";

            return Regex.Match(name, "[a-å]?[a-å]?[a-å]$").Value.ToUpper();
        }

        private async Task<bool> IsUserLoggedIn()
        {
            var user = await GetAuthenticatedUserAsync();
            return (user is not null && user.IsAuthenticated);
        }

        private async Task<IIdentity> GetAuthenticatedUserAsync()
        {
            var authState = await authenticationStateTask;
            return authState?.User.Identity;

        }
    }

}
