using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.About;
using PortfolioClient.DTO.About.Admin;
using PortfolioClient.Service.Attributes;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : BaseController
    {
        private readonly IReadService<AdminAboutDTO> _readService;
        private readonly IWriteService<CreateAboutDTO, UpdateAboutDTO> _writeService;

        public AboutController(IReadService<AdminAboutDTO> readService, IWriteService<CreateAboutDTO, UpdateAboutDTO> writeService,INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get All About", "Admin")]

        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("Abouts/GetAllAbout", "abouts");
            return View(datas);
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Create About", "Admin")]

        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost("[action]")]
        [AuthorizeRole("Create About", "Admin")]

        public async Task<IActionResult> CreateAbout(CreateAboutDTO createAboutDTO)
        {
            return await HandleFormAndApiRequestAsync(
                createAboutDTO,
                () => _writeService.CreateAsync("Abouts/CreateAbout", createAboutDTO),
                "Hakkında başarıyla eklendi.",
                "Hakkında eklenirken bir hata oluştu.",
                "CreateAbout"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Update About", "Admin")]

        public async Task<IActionResult> UpdateAbout(string id)
        {
            var about = await _readService.GetAsync("Abouts/GetByIdAbout/", id);
            return View(new UpdateAboutDTO
            {
                Id = about.Id,
                Title = about.Title,
                Detail = about.Detail,
                ImageUrl = about.ImageUrl,
                Url = about.Url
            });
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRole("Update About", "Admin")]

        public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            return await HandleFormAndApiRequestAsync(
                updateAboutDTO,
                () => _writeService.UpdateAsync("Abouts/UpdateAbout", updateAboutDTO),
                "Hakkında başarıyla güncellendi.",
                "Hakkında güncellenirken bir hata oluştu.",
                "UpdateAbout"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Delete About", "Admin")]

        public async Task<IActionResult> DeleteAbout(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("Abouts/RemoveAbout/", id),
                "Hakkında başarıyla silindi.",
                "Hakkında silinirken bir hata oluştu."
            );
        }
    }
}
