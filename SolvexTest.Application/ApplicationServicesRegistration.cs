using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SolvexTest.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

        }
    }
}
