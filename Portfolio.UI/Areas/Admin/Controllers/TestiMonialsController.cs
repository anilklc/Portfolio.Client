using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.TestiMonials.Admin;
using PortfolioClient.Service.Attributes;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/TestiMonials")]
    public class TestiMonialsController : BaseController
    {
        private readonly IReadService<AdminTestiMonialsDTO> _readService;
        private readonly IWriteService<CreateTestiMonialsDTO, UpdateTestiMonialsDTO> _writeService;

        public TestiMonialsController(IReadService<AdminTestiMonialsDTO> readService, IWriteService<CreateTestiMonialsDTO, UpdateTestiMonialsDTO> writeService, INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get All Testi Monials", "Admin")]

        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("TestiMonials/GetAllTestiMonials", "testiMonials");
            return View(datas);
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Create Testi Monials", "Admin")]
        public IActionResult CreateTestiMonials()
        {
            return View();
        }

        [HttpPost("[action]")]
        [AuthorizeRole("Create Testi Monials", "Admin")]
        public async Task<IActionResult> CreateTestiMonials(CreateTestiMonialsDTO createTestiMonialsDTO)
        {
            return await HandleFormAndApiRequestAsync(
                createTestiMonialsDTO,
                () => _writeService.CreateAsync("TestiMonials/CreateTestiMonials", createTestiMonialsDTO),
                "Yorum başarıyla eklendi.",
                "Yorum eklenirken bir hata oluştu.",
                "CreateTestiMonials"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Update Testi Monials", "Admin")]
        public async Task<IActionResult> UpdateTestiMonials(string id)
        {
            var testiMonials = await _readService.GetAsync("TestiMonials/GetByIdTestiMonials/", id);
            return View(new UpdateTestiMonialsDTO
            {
                Id = testiMonials.Id,
                ClientImage = testiMonials.ClientImage,
                ClientName = testiMonials.ClientName,
                Comment = testiMonials.Comment
            });
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRole("Update Testi Monials", "Admin")]
        public async Task<IActionResult> UpdateTestiMonials(UpdateTestiMonialsDTO updateTestiMonialsDTO)
        {
            return await HandleFormAndApiRequestAsync(
                updateTestiMonialsDTO,
                () => _writeService.UpdateAsync("TestiMonials/UpdateTestiMonials", updateTestiMonialsDTO),
                "Yorum başarıyla güncellendi.",
                "Yorum güncellenirken bir hata oluştu.",
                "UpdateTestiMonials"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Delete Testi Monials", "Admin")]
        public async Task<IActionResult> DeleteTestiMonials(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("TestiMonials/RemoveTestiMonials/", id),
                "Yorum başarıyla silindi.",
                "Yorum silinirken bir hata oluştu."
            );
        }
    }
}
