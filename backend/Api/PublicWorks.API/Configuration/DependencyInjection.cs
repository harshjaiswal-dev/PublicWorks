using Business.Service.Implementation;
using Business.Service.Interface;
using Data.GenericRepository;
using Data.UnitOfWork;
using NetTopologySuite;
using NetTopologySuite.Geometries;

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
             services.AddScoped<IPriorityService, PriorityService>();
             services.AddScoped<ICategoryService, CategoryService>();
                 services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
              services.AddSingleton<GeometryFactory>(
                NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326)
            );
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            
            
            return services;
        }
    }
}