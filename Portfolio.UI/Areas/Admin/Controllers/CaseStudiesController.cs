using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.CaseStudies.Admin;
using PortfolioClient.Service.Attributes;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CaseStudies")]
    public class CaseStudiesController : BaseController
    {
        private readonly IReadService<AdminCaseStudiesDTO> _readService;
        private readonly IWriteService<CreateCaseStudiesDTO, UpdateCaseStudiesDTO> _writeService;

        public CaseStudiesController(IReadService<AdminCaseStudiesDTO> readService, IWriteService<CreateCaseStudiesDTO, UpdateCaseStudiesDTO> writeService, INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get All CaseStudies", "Admin")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("CaseStudies/GetAllCaseStudie", "caseStudies");
            return View(datas);
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Create CaseStudies", "Admin")]
        public IActionResult CreateCaseStudies()
        {
            return View();
        }

        [HttpPost("[action]")]
        [AuthorizeRole("Create CaseStudies", "Admin")]

        public async Task<IActionResult> CreateCaseStudies(CreateCaseStudiesDTO createCaseStudiesDTO)
        {
            return await HandleFormAndApiRequestAsync(
                createCaseStudiesDTO,
                () => _writeService.CreateAsync("CaseStudies/CreateCaseStudie", createCaseStudiesDTO),
                "CaseStudies başarıyla eklendi.",
                "CaseStudies eklenirken bir hata oluştu.",
                "CreateCaseStudies"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Update CaseStudies", "Admin")]

        public async Task<IActionResult> UpdateCaseStudies(string id)
        {
            var caseStudies = await _readService.GetAsync("CaseStudies/GetByIdCaseStudie/", id);
            return View(new UpdateCaseStudiesDTO
            {
                Id = caseStudies.Id,
                Title = caseStudies.Title,
                Detail = caseStudies.Detail,
                ImageUrl = caseStudies.ImageUrl,
                Url = caseStudies.Url,
                BoxType = caseStudies.BoxType,
                Category = caseStudies.Category,  
            });
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRole("Update", "Admin")]

        public async Task<IActionResult> UpdateCaseStudies(UpdateCaseStudiesDTO updateCaseStudiesDTO)
        {
            return await HandleFormAndApiRequestAsync(
                updateCaseStudiesDTO,
                () => _writeService.UpdateAsync("CaseStudies/UpdateCaseStudie", updateCaseStudiesDTO),
                "CaseStudies başarıyla güncellendi.",
                "CaseStudies güncellenirken bir hata oluştu.",
                "UpdateCaseStudies"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Delete CaseStudies", "Admin")]

        public async Task<IActionResult> DeleteCaseStudies(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("CaseStudies/RemoveCaseStudies/", id),
                "CaseStudies başarıyla silindi.",
                "CaseStudies silinirken bir hata oluştu."
            );
        }
    }
}
