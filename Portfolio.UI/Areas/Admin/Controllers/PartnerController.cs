using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.Partner.Admin;
using PortfolioClient.Service.Attributes;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Partner")]
    public class PartnerController : BaseController
    {
        private readonly IReadService<AdminPartnerDTO> _readService;
        private readonly IWriteService<CreatePartnerDTO, UpdatePartnerDTO> _writeService;

    public PartnerController(IReadService<AdminPartnerDTO> readService, IWriteService<CreatePartnerDTO, UpdatePartnerDTO> writeService, INotyfService notyfService) : base(notyfService)
    {
        _readService = readService;
        _writeService = writeService;
    }

    [HttpGet("[action]")]
    [AuthorizeRole("Get All Partner", "Admin")]
    public async Task<IActionResult> Index()
    {
        var datas = await _readService.GetAllAsync("Partners/GetAllPartner", "partners");
        return View(datas);
    }

    [HttpGet("[action]")]
    [AuthorizeRole("Create Partner", "Admin")]
    public IActionResult CreatePartner()
    {
        return View();
    }

    [HttpPost("[action]")]
    [AuthorizeRole("Create Partner", "Admin")]
    public async Task<IActionResult> CreatePartner(CreatePartnerDTO createPartnerDTO)
    {
        return await HandleFormAndApiRequestAsync(
            createPartnerDTO,
            () => _writeService.CreateAsync("Partners/CreatePartner", createPartnerDTO),
            "Partner başarıyla eklendi.",
            "Partner eklenirken bir hata oluştu.",
            "CreatePartner"
        );
    }

    [HttpGet("[action]/{id}")]
    [AuthorizeRole("Update Partner", "Admin")]
    public async Task<IActionResult> UpdatePartner(string id)
    {
        var partner = await _readService.GetAsync("Partners/GetByIdPartner/", id);
        return View(new UpdatePartnerDTO
        {
            Id = partner.Id,
            ImageUrl = partner.ImageUrl,
            Name = partner.Name,
        });
    }

    [HttpPost("[action]/{id}")]
    [AuthorizeRole("Update Partner", "Admin")]
     public async Task<IActionResult> UpdatePartner(UpdatePartnerDTO updatePartnerDTO)
    {
        return await HandleFormAndApiRequestAsync(
            updatePartnerDTO,
            () => _writeService.UpdateAsync("Partners/UpdatePartner", updatePartnerDTO),
            "Partner başarıyla güncellendi.",
            "Partner güncellenirken bir hata oluştu.",
            "UpdatePartner"
        );
    }

    [HttpGet("[action]/{id}")]
    [AuthorizeRole("Delete Partner", "Admin")]
    public async Task<IActionResult> DeletePartner(string id)
    {
        return await HandleDeleteRequestAsync(
            id,
            id => _writeService.DeleteAsync("Partners/RemovePartner/", id),
            "Partner başarıyla silindi.",
            "Partner silinirken bir hata oluştu."
        );
    }
}
}
