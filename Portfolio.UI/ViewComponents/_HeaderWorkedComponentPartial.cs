using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.Partner;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.ViewComponents
{
    public class _HeaderWorkedComponentPartial : ViewComponent
    {
        private readonly IReadService<PartnerDTO> _readService;

        public _HeaderWorkedComponentPartial(IReadService<PartnerDTO> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _readService.GetAllAsync("Partners/GetAllPartner", "partners");
            return View(response);
        }
    }
}
