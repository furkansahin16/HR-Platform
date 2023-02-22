using DataAccess.EfContext;
using DataAccess.EfRepository.Abstract;
using DataAccess.EfRepository.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddDalServices(this IServiceCollection services)
        {
            services.AddDbContext<HRDbContext>(ServiceLifetime.Transient);
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IPermissionRepo, PermissionRepo>();
        }
    }
}
