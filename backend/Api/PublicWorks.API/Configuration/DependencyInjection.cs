using Business.Service.Implementation;
using Business.Service.Interface;
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
             services.AddScoped<IPriorityService, PriorityService>();
             services.AddScoped<ICategoryService, CategoryService>();
                 services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUoW, UoW>();
            services.AddScoped<IStatusService, StatusService>();
            
            return services;
        }
    }
}