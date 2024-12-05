using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.CaseStudies;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.ViewComponents
{
    public class _MainCaseStudiesComponentPartial : ViewComponent
    {
        private readonly IReadService<CaseStudiesDTO> _readService;

        public _MainCaseStudiesComponentPartial(IReadService<CaseStudiesDTO> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _readService.GetAllAsync("CaseStudies/GetAllCaseStudie", "caseStudies");
            return View(response);
        }
    }
}
