using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PortfolioClient.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.Service.Services
{
    public class ReadService<TEntity> : IReadService<TEntity>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient client;
        private readonly IHttpContextAccessor _contextAccessor;
        public ReadService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            client = _httpClientFactory.CreateClient("PortfolioApi");
            _contextAccessor = contextAccessor;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var accessToken = _contextAccessor.HttpContext.Request.Cookies["AccessToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<List<TEntity>> GetAllAsync(string endpoint, string objectName)
        {
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            if (string.IsNullOrEmpty(objectName))
            {
                return await response.Content.ReadFromJsonAsync<List<TEntity>>();
            }
            var jsonContent = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonContent);
            var objectArray = jsonObject[objectName];
            return objectArray?.ToObject<List<TEntity>>() ?? new List<TEntity>();
        }

        public async Task<TEntity> GetAsync(string endpoint, string id)
        {
            var response = await client.GetAsync($"{endpoint}{id}");
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }
    }
}
