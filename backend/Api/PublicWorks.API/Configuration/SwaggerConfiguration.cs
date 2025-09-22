using Microsoft.OpenApi.Models;

namespace PublicWorks.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PublicWorks.API",
                    Description = "API for Public Works - Issue Reporting & Work Order Management",
                });
            });

            return services;
        }
    }
}