using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortfolioClient.Service.Interfaces;
using PortfolioClient.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.Service
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("PortfolioApi", client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("BaseUrl").Value);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddSession();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped(typeof(IReadService<>), typeof(ReadService<>));
            services.AddScoped(typeof(IWriteService<,>), typeof(WriteService<,>));
        }
    }
}
