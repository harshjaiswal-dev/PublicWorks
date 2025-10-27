using Business.Service.Implementation;
using Business.Service.Interface;
using Data.GenericRepository;
using Data.UnitOfWork;
using PublicWorks.API.Helpers;

namespace PublicWorks.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IActionTypeService, ActionTypeService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<JwtService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<IRemarkService, RemarkService>();     
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserHelper,UserHelper>(); 
            
            
            //   services.AddSingleton<GeometryFactory>(
            //     NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326)
            // );

            return services;
        }
    }
}