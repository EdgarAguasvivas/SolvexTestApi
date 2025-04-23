using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolvexTest.Application.Contracts;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Domain.Entities.Auth;
using SolvexTest.Infrastructure.Mapping;
using SolvexTest.Infrastructure.Persistence;
using SolvexTest.Infrastructure.Persistence.Repositories;
using SolvexTest.Infrastructure.Services;

namespace SolvexTest.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericRepository<User>), typeof(GenericRepository<User>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
    }
}