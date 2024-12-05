using Microsoft.AspNetCore.Http;
using PortfolioClient.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.Service.Services
{
    public class WriteService<TCreate, TUpdate> : IWriteService<TCreate, TUpdate>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient client;
        private readonly IHttpContextAccessor _contextAccessor;
        public WriteService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            client = _httpClientFactory.CreateClient("PortfolioApi");
            _contextAccessor = contextAccessor;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _contextAccessor = contextAccessor;
            var accessToken = _contextAccessor.HttpContext.Request.Cookies["AccessToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        }

        public async Task<HttpResponseMessage> CreateAsync(string endpoint, TCreate entity)
        {
            var response = await client.PostAsJsonAsync(endpoint,entity);
            return response;
        }

        public async Task<bool> DeleteAsync(string endpoint, string id)
        {
            var response = await client.DeleteAsync($"{endpoint}{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> UpdateAsync(string endpoint, TUpdate entity)
        {
            var response = await client.PutAsJsonAsync(endpoint,entity);
            return response;
        }
    }
}
