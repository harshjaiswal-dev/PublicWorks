using System.Net;
using System.Text.Json;
using Serilog;

namespace PublicWorks.API.Middleware
{
    public class ExceptionMiddleware
    {      
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Pass request to the next middleware
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Handle exceptions
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Default to 500
            var statusCode = (int)HttpStatusCode.InternalServerError;

            // Log the error with Serilog
            Log.Error(ex,
                "Unhandled exception while processing {Method} {Path}. TraceId: {TraceId}",
                context.Request?.Method,
                context.Request?.Path.Value,
                context.TraceIdentifier);

            // Create safe API response
            var errorResponse = new
            {
                Message = "An unexpected error occurred. Please contact support.",
                TraceId = context.TraceIdentifier
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}