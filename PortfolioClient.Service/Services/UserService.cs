using Microsoft.AspNetCore.Http;
using PortfolioClient.DTO.Role;
using PortfolioClient.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IReadService<RoleDTO> _readService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IReadService<RoleDTO> readService, IHttpContextAccessor httpContextAccessor)
        {
            _readService = readService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<RoleDTO>> GetUserRolesAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {

                string? name = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                var roles = await _readService.GetAllAsync($"Users/GetRolesToUser/{name}", "roles");
                _httpContextAccessor.HttpContext?.Session.SetString("UserName", name);
                _httpContextAccessor.HttpContext?.Session.SetString("UserRoles", string.Join(",", roles.Select(r => r.Name)));
                return roles.ToList();
            }
            else
            {
                return new List<RoleDTO>();
            }

        }

    }
}
