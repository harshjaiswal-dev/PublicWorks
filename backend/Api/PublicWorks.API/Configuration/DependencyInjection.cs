using Business;
using Data.UnitOfWork;

namespace PublicWorks.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUoW, UoW>();
            
            return services;
        }
    }
}