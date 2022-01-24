using Microsoft.Extensions.DependencyInjection;
using pfp.Infrastructure.Repositories;
using pfp.Application.Interfaces;

namespace pfp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
