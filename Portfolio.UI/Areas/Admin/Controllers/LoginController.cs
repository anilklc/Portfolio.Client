using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.Login;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Login")]
    public class LoginController : BaseController
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService, INotyfService notyfService) : base(notyfService)
        {
            _authService = authService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            if (await _authService.HasTokenInCookie())
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }

            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Index(LoginDTO login)
        {
            return await HandleFormAndApiRequestAsync(
                login,
                () => _authService.AuthenticateAsync("Auth/Login/", login),
                "Giriş başarılı",
                "Giriş başarısız",
                nameof(Index));
        }

        public IActionResult Logout()
        {
            _authService.RemoveTokenFromCookie();
            return RedirectToAction("Index", "Login", "Admin");
        }
    }
}
