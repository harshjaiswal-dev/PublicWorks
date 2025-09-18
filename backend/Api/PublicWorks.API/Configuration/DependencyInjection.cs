namespace PublicWorks.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            // Register Business Layer
            // services.AddScoped<, EmployeeService>();
            // services.AddScoped<IAuthService, AuthService>();
            
            return services;
        }
    }
}