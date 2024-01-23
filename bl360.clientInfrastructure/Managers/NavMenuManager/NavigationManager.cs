
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using bl360.clientInfrastructure.Services;
using bl360.shared.Constants.Storage;
using bl360.domain;
using bl360.clientInfrastructure.Routes;
using System.Reflection.Metadata;

namespace bl360.clientInfrastructure.Managers.NavMenuManager
{
    public class NavMenuManager:INavMenuManager
    {
        private readonly IStorageService _secureStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _factory;
        private int userId { get; set; } = 0;
        private int companyId { get; set; } = 0;
        private string PriorityMode { get; set; } = "";


        public NavMenuManager(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            IHttpClientFactory factory,
            IStorageService secureStorage
            )
        {
            _httpClient = httpClient;
            _secureStorage = secureStorage;
            _authenticationStateProvider = authenticationStateProvider;
            _factory = factory;
            if (_httpClient.DefaultRequestHeaders.Count() == 0)
            {
                _httpClient.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);
            }
        }

        private void AssignClientData(HttpClient cl)
        {
            cl.BaseAddress = _httpClient.BaseAddress;
            cl.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            _httpClient.DefaultRequestHeaders.Authorization.Scheme,
            _httpClient.DefaultRequestHeaders.Authorization.Parameter);
            cl.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);
        }

        private async Task LoadUserAndcompany()
        {
            if(userId < 11 && companyId < 11)
            {
                String cid = await _secureStorage.GetItem("CID");
                String uid = await _secureStorage.GetItem("UID");
                PriorityMode = await _secureStorage.GetItem("PriorityMode");
                if(!string.IsNullOrEmpty(cid))
                {
                    companyId = Int32.Parse(cid);
                }
                if(!string.IsNullOrEmpty(uid))
                {
                    userId = Int32.Parse(uid);
                }
            }
        }

        public async Task<BLUIElement> GetMenuUIElement(ObjectFormRequest request)
        {
            try
            {
                string list = null;
                BLUIElement? list1 = null;
                var cl = _factory.CreateClient();
                //cl.BaseAddress = ;
                cl.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    _httpClient.DefaultRequestHeaders.Authorization.Scheme,
                    _httpClient.DefaultRequestHeaders.Authorization.Parameter);
                cl.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);
                var response = await cl.PostAsJsonAsync(BaseEndpoint.BaseURL + ObjectEndpoints.FormDefinitionURL, request);
                list = await response.Content.ReadAsStringAsync();
                
                if (list != null)
                {
                    list1 = JsonConvert.DeserializeObject<BLUIElement>(list);
                }
                if (list1 == null)
                {
                    return new BLUIElement();
                }
                else
                {
                    return list1;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
