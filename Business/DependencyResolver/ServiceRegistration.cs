 using Business.Mapper;
using Business.Services.Abstract;
using Business.Services.Concrete;
using DataAccess.DependencyResolver;
using Microsoft.Extensions.DependencyInjection;
using System.Security;

namespace Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddDalServices();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddAutoMapper(typeof(EmployeeProfile));
            services.AddAutoMapper(typeof(PermissionProfile));
        }
    }
}
