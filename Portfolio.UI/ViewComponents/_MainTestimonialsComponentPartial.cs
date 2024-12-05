using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.TestiMonials;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.ViewComponents
{
    public class _MainTestimonialsComponentPartial : ViewComponent
    {
        private readonly IReadService<TestiMonialsDTO> _readService;

        public _MainTestimonialsComponentPartial(IReadService<TestiMonialsDTO> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _readService.GetAllAsync("TestiMonials/GetAllTestiMonials", "testiMonials");
            return View(response);
        }
    }
}
