using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using bl360.clientInfrastructure.Services;
using bl360.shared.Constants.Storage;
using bl360.domain;
using bl360.clientInfrastructure.Routes;

namespace bl360.clientInfrastructure.Managers.ComboDataManager
{
    
    public class ComboDataManager : IComboDataManager
    {
        #region initialization
        private readonly HttpClient _httpClient;
        private readonly IStorageService _localStorage;
        private readonly IHttpClientFactory _factory;

        private string PriorityMode { get; set; } = "";
        private int companyId { get; set; } = 0;
        private int userId { get; set; } = 0;
        public ComboDataManager(HttpClient httpClient, IStorageService localStorage, IHttpClientFactory factory)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            if (_httpClient.DefaultRequestHeaders.Count() == 0)
                _httpClient.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);
            _factory = factory;
        }

        private async Task LoadUserAndcompany()
        {
            if(companyId < 11 && userId < 11)
            {
                string cid = await _localStorage.GetItem("CID");
                string uid = await _localStorage.GetItem("UID");
                PriorityMode = await _localStorage.GetItem("PriorityMode");
                companyId = int.Parse(cid);
                userId = int.Parse(uid);
            }
            
        }

        private void AssignClientData(HttpClient cl)
        {
            cl.BaseAddress = _httpClient.BaseAddress;
            cl.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                _httpClient.DefaultRequestHeaders.Authorization.Scheme,
                _httpClient.DefaultRequestHeaders.Authorization.Parameter);
            cl.DefaultRequestHeaders.Add("IntegrationID", GlobalConsts.intergrationId);
        }

        #endregion

   
        public async Task<IList<AddressResponse>> GetAddressResponses(ComboRequestDTO requestDTO)
        {
            try
            {
                var cl = _factory.CreateClient();
                AssignClientData(cl);
                var response = await cl.PostAsJsonAsync(requestDTO.RequestingURL, requestDTO);
                string content = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IList<AddressResponse>>(content);
                return list != null ? list : new List<AddressResponse>();
            }
            catch (Exception)
            {
                return new List<AddressResponse>();
            }
        }

        public async Task<IList<ItemResponse>> GetItemResponses(ComboRequestDTO requestDTO)
        {
            try
            {

                var cl = _factory.CreateClient();
                AssignClientData(cl);
                var response = await cl.PostAsJsonAsync(requestDTO.RequestingURL, requestDTO);
                string content = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IList<ItemResponse>>(content);
                return list != null ? list : new List<ItemResponse>();
            }
            catch (Exception ex)
            {
                return new List<ItemResponse>();
            }
        }

       


        public async Task<IList<User>> GetUserResponses(ComboRequestDTO requestDTO)
        {
            try
            {
                IList<User>? users = new List<User>();
                
                    string content;
                    var cl = _factory.CreateClient();
                    AssignClientData(cl);
                    var response = await cl.PostAsJsonAsync(requestDTO.RequestingURL, requestDTO);
                    content = await response.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<IList<User>>(content);

                

                return users != null ? users : new List<User>();
            }
            catch
            {
                return new List<User>();
            }


        }

        public async Task<IList<AccountResponse>> GetAccountResponse(ComboRequestDTO requestDTO)
        {
            string uid = await _localStorage.GetItem("UID");
            string cid = await _localStorage.GetItem("CID");
            int uId = int.Parse(uid);
            int cId = int.Parse(cid);
            string content;
            try
            {
                
                    var cl = _factory.CreateClient();
                    AssignClientData(cl);
                    var response = await cl.PostAsJsonAsync(requestDTO.RequestingURL, requestDTO);
                    content = await response.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<IList<AccountResponse>>(content);
                    return list != null ? list : new List<AccountResponse>();
                

            }
            catch
            {
                return null;
            }
        }
        public async Task<IList<CodeBaseResponse>> GetCodeBaseResponses(ComboRequestDTO requestDTO)
        {
            string uid = await _localStorage.GetItem("UID");
            string cid = await _localStorage.GetItem("CID");
            int uId = int.Parse(uid);
            int cId = int.Parse(cid);
            try
            {
                
                    string content;
                    var cl = _factory.CreateClient();
                    AssignClientData(cl);
                    var response = await cl.PostAsJsonAsync(requestDTO.RequestingURL, requestDTO);
                    content = await response.Content.ReadAsStringAsync();

                    var list = JsonConvert.DeserializeObject<IList<CodeBaseResponse>>(content);
                    return list;
                

            }
            catch
            {
                return new List<CodeBaseResponse>();
            }
        }

        public async Task<IList<UnitResponse>> GetItemUnits(ComboRequestDTO requestDTO)
        {
            try
            {
                    string uid = await _localStorage.GetItem("UID");
                    string cid = await _localStorage.GetItem("CID");
                    int uId = int.Parse(uid);
                    int cId = int.Parse(cid);

                    string content;
                    var cl = _factory.CreateClient();
                    AssignClientData(cl);
                    var response = await cl.PostAsJsonAsync(requestDTO.RequestingURL, requestDTO);
                    content = await response.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<IList<UnitResponse>>(content);

                    return list;
                

            }
            catch (Exception)
            {
                return new List<UnitResponse>();
            }

        }

        

        public async Task<IList<ItemCodeResponse>> GetItemByItemCode(ItemRequestModel itemRequest)
        {

            HttpResponseMessage? response = null;
            IList<ItemCodeResponse>? itemResponse;
            try
            {
                
                    string uid = await _localStorage.GetItem("UID");
                    string cid = await _localStorage.GetItem("CID");
                    int uId = int.Parse(uid);
                    int cId = int.Parse(cid);

                    string responseBody = "";

                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), TokenEndpoints.GetItemByItemCodeEndPoint))
                    {
                        request.Content = JsonContent.Create(itemRequest);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        response = await _httpClient.SendAsync(request);
                    }
                    responseBody = await response.Content.ReadAsStringAsync();
                    itemResponse = JsonConvert.DeserializeObject<IList<ItemCodeResponse>>(responseBody);
                



            }
            catch
            {

                itemResponse = null;

            }

            return itemResponse != null ? itemResponse : new List<ItemCodeResponse>();
        }
        

        public async Task<IList<BinaryDocument>> GetItemDocuments(ItemRequestModel Req)
        {
            HttpResponseMessage response = null;
            IList<BinaryDocument> ImagesResponse = new List<BinaryDocument>();
            try
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), TokenEndpoints.ItemRateEndPoint))
                {
                    request.Headers.TryAddWithoutValidation("Timestamp", DateTime.Now.Ticks.ToString());
                    request.Content = JsonContent.Create(Req);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    response = await _httpClient.SendAsync(request);
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                ImagesResponse = JsonConvert.DeserializeObject<IList<BinaryDocument>>(responseBody);


            }
            catch (Exception exp)
            {
                Console.WriteLine("exp is {0}", exp);


            }
            finally
            {
            }

            return ImagesResponse;
        }

        

        
    }

   
}
