using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.SocialMedia;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.ViewComponents
{
    public class _HeaderHomeComponentPartial : ViewComponent
    {
        private readonly IReadService<SocialMediaDTO> _readService;
        public _HeaderHomeComponentPartial(IReadService<SocialMediaDTO> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _readService.GetAllAsync("SocialMedias/GetAllSocialMedia", "socialMedias");
            return View(response);
        }
    }
}
