using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.AuthorizeMenu;
using PortfolioClient.DTO.Role;
using PortfolioClient.Service.Attributes;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AuthorizeMenu")]
    public class AuthorizeMenuController : BaseController
    {
        private readonly IReadService<ApplicationService> _readService;
        private readonly IReadService<Role> _roleService;
        private readonly IWriteService<AssignRole, AssignRole> _writeService;

        public AuthorizeMenuController(IReadService<ApplicationService> readService, IReadService<Role> roleService, INotyfService notyfService, IWriteService<AssignRole, AssignRole> writeService) : base(notyfService)
        {
            _readService = readService;
            _roleService = roleService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get Authorize Definition", "Admin")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("ApplicationServices/GetAuthorizeDefinitionEndpoints", "");
            return View(datas);
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get Roles", "Admin")]
        public async Task<IActionResult> GetRolesForMenu(string menu, string code)
        {
            var allRoles = await _roleService.GetAllAsync("Roles/GetAllRoles", "roles");
            var assignedRoles = await _roleService.GetAllAsync($"AuthorizationEndpoints/GetRolesToEndpoint/{menu}/{code}", "roles");
            var assignedRoleIds = assignedRoles?.Select(r => r.Id).ToArray() ?? new string[] { };
            var result = new
            {
                AllRoles = allRoles,
                AssignedRoleIds = assignedRoleIds
            };
            return Json(result);
        }

        [HttpPost("[action]")]
        [AuthorizeRole("Assign Role", "Admin")]
        public async Task<IActionResult> AssignRoles([FromBody] AssignRole assignRole)
        {
            return await HandleFormAndApiRequestAsync(
                 assignRole,
                 () => _writeService.CreateAsync("AuthorizationEndpoints/AssignRoleEndpoint", assignRole),
                 "Roller başarıyla güncellendi.",
                 "Roller güncellenirken bir hata oluştu."
             );
        }
    }
}
