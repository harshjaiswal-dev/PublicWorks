using Microsoft.Extensions.Configuration;
using PublicWorks.API.Configuration;
using PublicWorks.API.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddSerilog(options =>
{
    //we can configure serilog from configuration
    options.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddAuthentication();
builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "Google";
})
.AddCookie("Cookies")
.AddGoogle(googleOptions =>
{
    Console.WriteLine(builder.Configuration["Authentication:Google:ClientId"]);
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Google ClientId not found");
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret not found");
});
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            c.RoutePrefix = ""; 
        });
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
