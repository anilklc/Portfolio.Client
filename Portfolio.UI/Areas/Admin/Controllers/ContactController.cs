using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PortfolioClient.DTO.GetInTouch.Admin;
using PortfolioClient.Service.Attributes;
using PortfolioClient.Service.Interfaces;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    public class ContactController : BaseController
    {
        private readonly IReadService<AdminGetInTouchDTO> _readService;
        private readonly IWriteService<CreateGetInTouchDTO, UpdateGetInTouchDTO> _writeService;

        public ContactController(IReadService<AdminGetInTouchDTO> readService, IWriteService<CreateGetInTouchDTO, UpdateGetInTouchDTO> writeService, INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get All Contact", "Admin")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("Contact/GetAllContact", "contacts");
            return View(datas);
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Create Contact", "Admin")]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost("[action]")]
        [AuthorizeRole("Create Contact", "Admin")]
        public async Task<IActionResult> CreateContact(CreateGetInTouchDTO createContactDTO)
        {
            return await HandleFormAndApiRequestAsync(
                createContactDTO,
                () => _writeService.CreateAsync("Contact/CreateContact", createContactDTO),
                "Contact başarıyla eklendi.",
                "Contact eklenirken bir hata oluştu.",
                "CreateContact"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Update Contact", "Admin")]
        public async Task<IActionResult> UpdateContact(string id)
        {
            var Contact = await _readService.GetAsync("Contact/GetByIdContact/", id);
            return View(new UpdateGetInTouchDTO
            {
                Id = Contact.Id,
                Email = Contact.Email,
                Message = Contact.Message,
                Mobile = Contact.Mobile
            });
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRole("Update Contact", "Admin")]
        public async Task<IActionResult> UpdateContact(UpdateGetInTouchDTO updateContactDTO)
        {
            return await HandleFormAndApiRequestAsync(
                updateContactDTO,
                () => _writeService.UpdateAsync("Contact/UpdateContact", updateContactDTO),
                "Contact başarıyla güncellendi.",
                "Contact güncellenirken bir hata oluştu.",
                "UpdateContact"
            );
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Delete Contact", "Admin")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("Contact/RemoveContact/", id),
                "Contact başarıyla silindi.",
                "Contact silinirken bir hata oluştu."
            );
        }
    }
}
