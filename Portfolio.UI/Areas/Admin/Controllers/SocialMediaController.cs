using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.SocialMedia.Admin;
using PortfolioClient.Service.Attributes;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SocialMedia")]
    public class SocialMediaController : BaseController
    {
        private readonly IReadService<AdminSocialMediaDTO> _readService;
        private readonly IWriteService<CreateSocialMediaDTO, UpdateSocialMediaDTO> _writeService;

        public SocialMediaController(IReadService<AdminSocialMediaDTO> readService, IWriteService<CreateSocialMediaDTO, UpdateSocialMediaDTO> writeService, INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get All Social Media", "Admin")]

        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("SocialMedias/GetAllSocialMedia", "socialMedias");
            return View(datas);
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Create Social Media", "Admin")]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost("[action]")]
        [AuthorizeRole("Create Social Media", "Admin")]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDTO createSocialMediaDTO)
        {
            return await HandleFormAndApiRequestAsync(
                createSocialMediaDTO,
                () => _writeService.CreateAsync("SocialMedias/CreateSocialMedia", createSocialMediaDTO),
                "Sosyal Medya başarıyla eklendi.",
                "Sosyal Medya eklenirken bir hata oluştu.",
                "CreateSocialMedia"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Update Social Media", "Admin")]
        public async Task<IActionResult> UpdateSocialMedia(string id)
        {
            var socialMedia = await _readService.GetAsync("SocialMedias/GetByIdSocialMedia/", id);
            return View(new UpdateSocialMediaDTO
            {
                Id = socialMedia.Id,
                SocialMediaIcon = socialMedia.SocialMediaIcon,
                SocialMediaName = socialMedia.SocialMediaName,
                Url = socialMedia.Url
            });
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRole("Update Social Media", "Admin")]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDTO updateSocialMediaDTO)
        {
            return await HandleFormAndApiRequestAsync(
                updateSocialMediaDTO,
                () => _writeService.UpdateAsync("SocialMedias/UpdateSocialMedia", updateSocialMediaDTO),
                "Sosyal Medya başarıyla güncellendi.",
                "Sosyal Medya güncellenirken bir hata oluştu.",
                "UpdateSocialMedia"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Delete Social Media", "Admin")]
        public async Task<IActionResult> DeleteSocialMedia(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("SocialMedias/RemoveSocialMedia/", id),
                "Sosyal Medya başarıyla silindi.",
                "Sosyal Medya silinirken bir hata oluştu."
            );
        }
    }
}
