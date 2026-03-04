
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log incoming request method and path
            var method = context.Request.Method;
            var path = context.Request.Path;
            var requestLog = $"[REQUEST] {method} {path}";
            Console.WriteLine(requestLog);
            _logger.LogInformation(requestLog);

            // Call the next middleware
            await _next(context);

            // After pipeline completes, log response status code
            var statusCode = context.Response?.StatusCode;
            var responseLog = $"[RESPONSE] {statusCode}";
            Console.WriteLine(responseLog);
            _logger.LogInformation(responseLog);
        }
    }

    // Extension method for registration
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}