using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.GetInTouch;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.ViewComponents
{
    public class _MainGetInTouchComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

        
    }

}
