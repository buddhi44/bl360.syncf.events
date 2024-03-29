﻿using bl360.clientInfrastructure.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.maui.Services
{
    public class SecureStorageService : IStorageService
    {
        public async ValueTask<bool> ContainKeyAsync(string key)
        {
            try
            {
                string token = await SecureStorage.GetAsync(key);
                return !string.IsNullOrEmpty(token);
            }
            catch
            {
                // Handle the exception here
                return false;
            }

        }
        public async Task<string> GetItem(string key)
        {
            string result = await SecureStorage.Default.GetAsync(key);
            if (string.IsNullOrEmpty(result)) return default;

            try
            {
                return JsonConvert.DeserializeObject<string>(result);
            }
            catch
            {
                return "";
            }
        }
        public async Task<T> GetItemAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            string result = await SecureStorage.GetAsync(key);
            //return JsonConvert.DeserializeObject<T>(result);

            if (string.IsNullOrEmpty(result)) return default;

            try
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch
            {
                return (T)(object)result;
            }
        }
        public async Task RemoveItem(string key)
        {
            SecureStorage.Default.Remove(key);
            await Task.CompletedTask;
        }
        public async Task SetItem<T>(string key, T item)
        {
            string content = JsonConvert.SerializeObject(item);
            await SecureStorage.Default.SetAsync(key, content);
        }
        public async ValueTask SetItemAsync<T>(string key, T data)
        {
            var serialisedData = JsonConvert.SerializeObject(data);
            await SecureStorage.Default.SetAsync(key, serialisedData).ConfigureAwait(false);
        }
        public async Task RemoveAll()
        {
            SecureStorage.Default.RemoveAll();
        }
    }
}
