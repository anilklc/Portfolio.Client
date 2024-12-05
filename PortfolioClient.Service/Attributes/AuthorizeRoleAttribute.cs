using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PortfolioClient.Service.Interfaces;
using PortfolioClient.Service.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PortfolioClient.Service.Attributes
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string _rolesToUser;
        private readonly string _areas;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeRoleAttribute(string roleToUser, string? areas)
        {
            _httpContextAccessor = new HttpContextAccessor();
            _rolesToUser = roleToUser;
            _areas = areas;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
            var notyfService = context.HttpContext.RequestServices.GetService(typeof(INotyfService)) as INotyfService;

            if (token != null)
            {
                var hasPermission = GetControlUser(context, token).Result;
                if (!hasPermission)
                {
                    //notyfService.Information("Bu sayfaya girmeye yetkiniz yok, lütfen yöneticinizle iletişime geçiniz.");
                    context.Result = new ViewResult
                    {
                        ViewName = "Unauthorized"
                    };
                    return;
                }
            }
            else
            {
                notyfService.Error("Lütfen önce giriş yapınız");
                context.Result = _areas.Equals("Admin")
                    ? new RedirectToActionResult("Index", "Login", "Admin")
                    : new RedirectToActionResult("Index", "Login", null);
                return;
            }
        }

        private async Task<bool> GetControlUser(ActionExecutingContext context, string token)
        {
            var userRoles = _httpContextAccessor.HttpContext.Session.GetString("UserRoles");
            var username = _httpContextAccessor.HttpContext.Session.GetString("UserName");

            if (userRoles == null && username == null)
            {
                var userService = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
                var rolesList = await userService.GetUserRolesAsync(token);


                userRoles = string.Join(",", rolesList.Select(role => role.Name));
                _httpContextAccessor.HttpContext.Session.SetString("UserRoles", userRoles);
                username = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            }

            if (username == "admin")
            {
                return true;
            }

            if (userRoles == null)
            {
                return false;
            }

            var rolesArray = userRoles.Split(",");
            return rolesArray.Any(r => r.Equals(_rolesToUser));
        }
    }

}
