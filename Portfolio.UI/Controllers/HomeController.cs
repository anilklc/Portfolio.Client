using System.Diagnostics;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Portfolio.UI.Models;
using PortfolioClient.DTO.GetInTouch;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWriteService<GetInTouchDTO, GetInTouchDTO> _writeService;
        private readonly INotyfService _notyfService;
        public HomeController(IWriteService<GetInTouchDTO, GetInTouchDTO> writeService, INotyfService notyfService)
        {
            _writeService = writeService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(GetInTouchDTO getInTouchDTO)
        {
            var response = await _writeService.CreateAsync("Contact/CreateContact", getInTouchDTO);
            if (response.IsSuccessStatusCode)
            {
                _notyfService.Success("Mesaj�n�z g�nderildi en k�sa s�rede d�n�� yap�l�cakt�r.");
            }
            else
            {
                _notyfService.Error("Mesaj�n�z g�nderilemedi l�tfen tekrar deneyiniz.");
            }
            return RedirectToAction("Index");
        }

    }
}
