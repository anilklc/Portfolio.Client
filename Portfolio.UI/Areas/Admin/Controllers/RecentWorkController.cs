using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.RecentWork.Admin;
using PortfolioClient.Service.Attributes;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/RecentWork")]
    public class RecentWorkController : BaseController
    {
        private readonly IReadService<AdminRecentWorkDTO> _readService;
        private readonly IWriteService<CreateRecentWorkDTO, UpdateRecentWorkDTO> _writeService;

        public RecentWorkController(IReadService<AdminRecentWorkDTO> readService, IWriteService<CreateRecentWorkDTO, UpdateRecentWorkDTO> writeService, INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get All Recent Work", "Admin")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("RecentWorks/GetAllRecentWork", "recents");
            return View(datas);
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Create Recent Work", "Admin")]
        public IActionResult CreateRecentWork()
        {
            return View();
        }

        [HttpPost("[action]")]
        [AuthorizeRole("Create Recent Work", "Admin")]
        public async Task<IActionResult> CreateRecentWork(CreateRecentWorkDTO createRecentWorkDTO)
        {
            return await HandleFormAndApiRequestAsync(
                createRecentWorkDTO,
                () => _writeService.CreateAsync("RecentWorks/CreateRecentWork", createRecentWorkDTO),
                "Recentwork başarıyla eklendi.",
                "Recentwork eklenirken bir hata oluştu.",
                "CreateRecentWork"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Update Recent Work", "Admin")]
        public async Task<IActionResult> UpdateRecentWork(string id)
        {
            var recentWork = await _readService.GetAsync("RecentWorks/GetByIdRecentWork/", id);
            return View(new UpdateRecentWorkDTO
            {
                Id = recentWork.Id,
                Title = recentWork.Title,
                Detail = recentWork.Detail,
                ImageUrl = recentWork.ImageUrl,
                Url = recentWork.Url
            });
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRole("Update Recent Work", "Admin")]
        public async Task<IActionResult> UpdateRecentWork(UpdateRecentWorkDTO updateRecentWorkDTO)
        {
            return await HandleFormAndApiRequestAsync(
                updateRecentWorkDTO,
                () => _writeService.UpdateAsync("RecentWorks/UpdateRecentWork", updateRecentWorkDTO),
                "Recentwork başarıyla güncellendi.",
                "Recentwork güncellenirken bir hata oluştu.",
                "UpdateRecentWork"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Delete Recent Work", "Admin")]
        public async Task<IActionResult> DeleteRecentWork(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("RecentWorks/RemoveRecentWork/", id),
                "Recentwork başarıyla silindi.",
                "Recentwork silinirken bir hata oluştu."
            );
        }
    }
}
