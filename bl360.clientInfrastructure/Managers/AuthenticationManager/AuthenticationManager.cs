using bl360.clientInfrastructure.Authentication;
using bl360.clientInfrastructure.Routes;
using bl360.domain;
using bl360.shared.Constants.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using bl360.clientInfrastructure.Services;
using System.Net.Http.Json;

namespace bl360.clientInfrastructure.Managers.AuthenticationManager
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly HttpClient _httpClient;
        private readonly IStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationManager(HttpClient httpClient,
            IStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            if (_httpClient.DefaultRequestHeaders.Contains("IntegrationID"))
            {
                _httpClient.DefaultRequestHeaders.Remove("IntegrationID");
            }
            _httpClient.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);

        }


        public async Task<ClaimsPrincipal> CurrentUser()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return state.User;
        }

        public async Task<CompletedUserAuth> GetUserInformation()
        {
            try
            {
                var response = await _httpClient.GetAsync(TokenEndpoints.UserInfoReadURL);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CompletedUserAuth>(content);
                await _localStorage.SetItem(StorageConstants.Local.UID, result.AuthenticatedUser.UserKey.ToString());

                if (result.AuthenticatedCompany != null && result.AuthenticatedCompany.CompanyKey != null)
                {
                    await _localStorage.SetItem(StorageConstants.Local.CID, result.AuthenticatedCompany.CompanyKey.ToString());
                }


                return result;
            }
            catch
            {
                return new CompletedUserAuth();
            }
        }

        public async Task<TokenResponse> Login(TokenRequest model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.AuthenticateURL, model);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(content);

                // var result = await response.ToResult<TokenResponse>();
                if (result != null && result.IsSuccess)
                {
                    var token = result.Token ?? "";
                    var refreshToken = result.RefreshToken ?? "";
                    var userImageURL = result.UserImageURL ?? "";
                    await _localStorage.SetItemAsync(StorageConstants.Local.AuthToken, token);
                    await _localStorage.SetItem(StorageConstants.Local.RefreshToken, refreshToken);

                    if (!string.IsNullOrEmpty(userImageURL))
                    {
                        await _localStorage.SetItemAsync(StorageConstants.Local.UserImageURL, userImageURL);
                    }
                    // c = _authenticationStateProvider.GetType();

                    await((BL10AuthProvider)this._authenticationStateProvider).StateChangedAsync();

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    

                    return result;
                }
                else
                {
                    return new TokenResponse();
                }
            }

            catch
            {
                return new TokenResponse();
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItem(StorageConstants.Local.AuthToken);
            await _localStorage.RemoveItem(StorageConstants.Local.RefreshToken);
            await _localStorage.RemoveItem(StorageConstants.Local.UserImageURL);
            await _localStorage.RemoveItem(StorageConstants.Local.CompanyName);
            await _localStorage.RemoveItem(StorageConstants.Local.SelectedShift);
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);
            var refreshToken = await _localStorage.GetItemAsync<string>(StorageConstants.Local.RefreshToken);

            var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.Refresh, new RefreshTokenRequest { Token = token, RefreshToken = refreshToken });

            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TokenResponse>(content);
            if (result != null)
            {
                token = result.Token;
                refreshToken = result.RefreshToken;
                await _localStorage.SetItemAsync(StorageConstants.Local.AuthToken, token);
                await _localStorage.SetItemAsync(StorageConstants.Local.RefreshToken, refreshToken);
                //await ((BL10AuthProvider)this._authenticationStateProvider).StateChangedAsync();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                throw new ApplicationException("Something went wrong during the refresh token action");
            }


            return token;
        }

        public async Task<string> TryForceRefreshToken()
        {
            return await RefreshToken();
        }

        public async Task<string> TryRefreshToken()
        {
            var availableToken = await _localStorage.GetItemAsync<string>(StorageConstants.Local.RefreshToken);
            if (string.IsNullOrEmpty(availableToken)) return string.Empty;
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var exp = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var timeUTC = DateTime.UtcNow;
            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 1)
                return await RefreshToken();
            return string.Empty;
        }
    }
}
