using Microsoft.Extensions.DependencyInjection;

using Data.Interfaces;
using Implementations.Repositories;
using Data.GenericRepository;
using Business.Interface;
using Business.Services;


namespace Data.Repositories.Configuration
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            //services.AddScoped<IIssueRepository, IsuueRepository>(); // Fix typo if needed
            services.AddScoped<IRemarkRepository, RemarkRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
           services.AddScoped<IStatusService,StatusService>();
            //services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();
            
     

            return services;
        }
    }
}
