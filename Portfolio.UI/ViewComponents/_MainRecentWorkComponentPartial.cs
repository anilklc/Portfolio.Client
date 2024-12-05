using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.RecentWork;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.ViewComponents
{
    public class _MainRecentWorkComponentPartial : ViewComponent
    {
        private readonly IReadService<RecentWorkDTO> _readService;
        public _MainRecentWorkComponentPartial(IReadService<RecentWorkDTO> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _readService.GetAllAsync("RecentWorks/GetAllRecentWork", "recents");
            return View(response);
        }
    }
}
