using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.About;
using PortfolioClient.Service.Interfaces;


namespace Portfolio.UI.ViewComponents
{
    public class _HeaderContentComponentPartial : ViewComponent
    {    
        private readonly IReadService<AboutDTO> _readService;
        public _HeaderContentComponentPartial(IReadService<AboutDTO> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _readService.GetAllAsync("Abouts/GetAllAbout","abouts");
            return View(response);
        }
    }
}
