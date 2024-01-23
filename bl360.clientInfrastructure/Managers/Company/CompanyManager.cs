using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using bl360.clientInfrastructure.Services;
using bl360.shared.Constants.Storage;
using bl360.domain;
using bl360.clientInfrastructure.Routes;
using bl360.clientInfrastructure.Authentication;


namespace bluelotus360.Com.commonLib.Managers.Company
{
    public class CompanyManager:ICompanyManager
    {
        private readonly HttpClient _httpClient;
        private IStorageService _localStorage;
        private AuthenticationStateProvider _authenticationStateProvider;
        private readonly IHttpClientFactory _factory;
        public CompanyManager(HttpClient httpClient,
            IStorageService localStorage, 
            AuthenticationStateProvider authenticationStateProvider,
            IHttpClientFactory factory)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;

            if (_httpClient.DefaultRequestHeaders.Count() == 0)
                _httpClient.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);
            _factory = factory;
        }
        private void AssignClientData(HttpClient cl)
        {
            cl.BaseAddress = _httpClient.BaseAddress;
            cl.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                _httpClient.DefaultRequestHeaders.Authorization.Scheme,
                _httpClient.DefaultRequestHeaders.Authorization.Parameter);
            cl.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);
        }
        //public async Task<ReportCompanyDetailsResponse>
        public async Task<IList<CompanyResponse>> GetUserCompanies()
        {
            CompanyResponse resp = new CompanyResponse();
            var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.CompanyListingEndPoint, resp);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                IList<CompanyResponse> companies = JsonConvert.DeserializeObject<IList<CompanyResponse>>(content);
                return companies;
            }
            else
            {
                return new List<CompanyResponse>();
            }

        }
       

        public async Task UpdateSelectedCompany(CompanyResponse companyResponse)
        {
            var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.CompanySelectedEndPoint, companyResponse);
            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TokenResponse>(content);

            // var result = await response.ToResult<TokenResponse>();
            if (result.IsSuccess)
            {
                var token = result.Token;
                var refreshToken = result.RefreshToken;
                var userImageURL = "";
                await _localStorage.SetItemAsync(StorageConstants.Local.AuthToken, token);
                await _localStorage.SetItem(StorageConstants.Local.RefreshToken, refreshToken);
                await _localStorage.SetItemAsync(StorageConstants.Local.CompanyName, companyResponse.CompanyName);
                await _localStorage.SetItemAsync("CompanyKey", companyResponse.CompanyKey);
                if (!string.IsNullOrEmpty(userImageURL))
                {
                    await _localStorage.SetItemAsync(StorageConstants.Local.UserImageURL, userImageURL);
                }

                await ((BL10AuthProvider)this._authenticationStateProvider).StateChangedAsync();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            }
            else
            {

            }
        }
        

        
    }
}
