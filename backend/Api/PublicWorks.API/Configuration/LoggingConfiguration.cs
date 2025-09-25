using Serilog;

namespace PublicWorks.API.Configuration
{
    public static class LoggingConfiguration
    {
        public static WebApplicationBuilder AddLoggingConfiguration(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog(static (context, services, configuration) =>
            {
                // Read Serilog settings from the appsettings.json file.
                configuration.ReadFrom.Configuration(context.Configuration);
                // Integrate with dependency injection for any sinks that require additional services.
                configuration.ReadFrom.Services(services);
                // Optionally, enable asynchronous logging for additional sinks defined in code.
                configuration.WriteTo.Async(a =>
                {
                    a.Console();  // Wrap Console sink asynchronously.
                    a.File("logs/log-.txt",
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: 30);  // Wrap File sink asynchronously.
                });
            });

            return builder;
        }
    }
}