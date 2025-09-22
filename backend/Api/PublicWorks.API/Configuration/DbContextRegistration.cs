using Data;
using Microsoft.EntityFrameworkCore;

namespace PublicWorks.API.Configuration 
{
    public static class DbContextRegistration
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
