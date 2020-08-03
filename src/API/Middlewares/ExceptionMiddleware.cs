using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid request. {Error}", ex.Message);
                await RespondAsync(context, 400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception encountred while handling request. {Error}", ex.Message);
                await RespondAsync(context, 500, ex.Message);
            }
        }

        private static Task RespondAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "text/plain";
            return context.Response.WriteAsync($"Error encountred while executing request. {message}");
        }
    }
}