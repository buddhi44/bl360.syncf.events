
using bl360.clientInfrastructure.Managers.Codebase;
using bl360.clientInfrastructure.Routes;
using bl360.clientInfrastructure.Services;
using bl360.domain;
using bl360.shared.Constants.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace bl360.clientInfrastructure
{
    public class CodebaseManager: ICodebaseManager
    {
        private readonly HttpClient _httpClient;
        //private readonly ISecureStorageService _localStorage;
        private readonly IStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CodebaseManager(HttpClient httpClient,
            IStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;

            if (_httpClient.DefaultRequestHeaders.Count() == 0)
                _httpClient.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);
        }

        public async Task<IList<CodeBaseResponse>> GetCodesByConditionCode(CodeBaseResponse record)
        {
            IList<CodeBaseResponse> codes=new List<CodeBaseResponse>();
            try
            {

                var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.GetCodesByConditionCode, record);
                await response.Content.LoadIntoBufferAsync();
                string content = response.Content.ReadAsStringAsync().Result;
                codes = JsonConvert.DeserializeObject<IList<CodeBaseResponse>>(content);



            }
            catch
            {

            }
            return codes;
        }
        //TODO:Have to make offline here
        public async Task<CodeBaseResponse> GetCodesByConditionCodeAndOurCode(CodeBaseResponse record)
        {
            CodeBaseResponse codes = new CodeBaseResponse();
            try
            {

                var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.GetCodesByConditionCodeAndOurCode, record);
                await response.Content.LoadIntoBufferAsync();
                string content = response.Content.ReadAsStringAsync().Result;
                codes = JsonConvert.DeserializeObject<CodeBaseResponse>(content);



            }
            catch
            {

            }
            return codes;
        }
    }
}
