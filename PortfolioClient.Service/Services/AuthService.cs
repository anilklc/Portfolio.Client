using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PortfolioClient.DTO.Login;
using PortfolioClient.DTO.Token;
using PortfolioClient.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        public AuthService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<HttpResponseMessage> AuthenticateAsync(string endpoint, LoginDTO login)
        {

            var client = _httpClientFactory.CreateClient("PortfolioApi");
            var response = await client.PostAsJsonAsync(endpoint, login);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonContent);
                var token = jsonObject["token"]?.ToObject<Token>();

                if (token != null)
                {
                    SetTokenInCookie(token);
                }
            }

            return response;
        }

        public async Task<bool> HasTokenInCookie()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("AccessToken"))
            {
                return true;
            }
            return false;
        }


        public void RemoveTokenFromCookie()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("AccessToken");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("RefreshToken");
            _httpContextAccessor.HttpContext.Session.Remove("UserRoles");
            _httpContextAccessor.HttpContext.Session.Remove("UserName");
        }

        public void SetTokenInCookie(Token token)
        {
            var cookie = new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddHours(5),
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("AccessToken", token.AccessToken, cookie);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", token.RefreshToken, cookie);
            _userService.GetUserRolesAsync(token.AccessToken);

        }
    }
}
