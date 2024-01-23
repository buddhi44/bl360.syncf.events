using bl360.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace bl360.clientInfrastructure.Managers.AuthenticationManager
{
    public interface IAuthenticationManager:IManager
    {
        Task<TokenResponse> Login(TokenRequest model);
        Task Logout();

        Task<string> RefreshToken();

        Task<string> TryRefreshToken();

        Task<string> TryForceRefreshToken();
        Task<ClaimsPrincipal> CurrentUser();

        Task<CompletedUserAuth> GetUserInformation();
    }
}
