using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Portfolio.UI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected readonly INotyfService notyfService;

        public BaseController(INotyfService notyfService)
        {
            this.notyfService = notyfService;
        }

        protected async Task<IActionResult> HandleFormAndApiRequestAsync<TModel>(TModel model, Func<Task<HttpResponseMessage>> apiCall, string successMessage, string errorMessage, string viewName = null)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    notyfService.Error(error);
                }

                return View(viewName ?? nameof(model), model);
            }

            var apiResponse = await apiCall();

            if (apiResponse.IsSuccessStatusCode)
            {
                notyfService.Success(successMessage);
                return RedirectToAction("Index");
            }
            else if (apiResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                notyfService.Error("Yetkilendirme hatası lütfen yöneticinizle iletişime geçiniz");
                return RedirectToAction("Index");
            }
            else
            {
                notyfService.Error(errorMessage);
                return View(viewName ?? nameof(model), model);
            }
        }
        protected async Task<IActionResult> HandleDeleteRequestAsync(string id, Func<string, Task<bool>> apiCall, string successMessage, string errorMessage)
        {
            var apiResponse = await apiCall(id);

            if (apiResponse)
            {
                notyfService.Success(successMessage);
            }
            else
            {
                notyfService.Error(errorMessage);
            }

            return RedirectToAction("Index");
        }
    }
}
