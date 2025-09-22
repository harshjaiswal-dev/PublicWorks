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
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new { Message = "An unexpected error occurred.", Details = ex.Message, InnerException = ex.InnerException?.Message };
            await context.Response.WriteAsJsonAsync(response);
        }      
    }
}