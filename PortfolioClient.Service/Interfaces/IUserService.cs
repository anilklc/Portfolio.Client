using PortfolioClient.DTO.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<RoleDTO>> GetUserRolesAsync(string token);
    }
}
