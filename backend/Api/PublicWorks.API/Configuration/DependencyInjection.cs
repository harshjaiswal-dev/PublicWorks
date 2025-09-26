using Business.Service.Implementation;
using Business.Service.Interface;
using Data.GenericRepository;
using Data.UnitOfWork;

namespace PublicWorks.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IActionTypeService, ActionTypeService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IRemarkService, RemarkService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<JwtService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}