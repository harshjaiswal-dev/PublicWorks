using Data.Helpers.Configuration;
using Data.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;
using PublicWorks.API.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddHelpers();
// // Call the static Serilog configuration method
// SerilogConfiguration.ConfigureLogger(builder.Configuration);

// builder.Host.UseSerilog(); // Use Serilog as the logging provider

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start correctly");
    Console.WriteLine("Startup exception: " + ex.Message);
}
finally
{
    Log.CloseAndFlush();
}


