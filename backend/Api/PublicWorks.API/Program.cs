using PublicWorks.API.Configuration;
using PublicWorks.API.Middleware;
using Serilog;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Serilog Configuration
// -------------------------
// Configure Serilog for structured logging from appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

// -------------------------
// Add Services to DI Container
// -------------------------

// Configure DbContext and database-related services
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Register business layer and repository dependencies
builder.Services.AddDependencyInjection();

// Configure JWT authentication (Bearer tokens)
builder.Services.AddJwtAuthentication(builder.Configuration);

// Configure Swagger/OpenAPI for API documentation
builder.Services.AddSwaggerConfiguration();

// Configure CORS policies to allow requests from frontend
builder.Services.AddCorsPolicies();

// Register HttpClientFactory for making HTTP requests
builder.Services.AddHttpClient();

// Register controllers and configure JSON serialization
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new GeometryJsonConverter());
    });

// Register HttpContextAccessor to access HTTP context in services
builder.Services.AddHttpContextAccessor();

// -------------------------
// Build the App
// -------------------------
var app = builder.Build();

// -------------------------
// Configure Middleware / HTTP Pipeline
// -------------------------

// Enable Swagger UI only in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = "";
    });
}

// Serilog request logging (logs all HTTP requests)
app.UseSerilogRequestLogging();

// Global exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enable CORS using configured policy
app.UseCors("AllowFrontend");

// Enable authentication and authorization middleware
// Important: Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

// Run the application
app.Run();
