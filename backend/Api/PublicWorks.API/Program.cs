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

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
