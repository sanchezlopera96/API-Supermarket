using Supermarket.Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace Supermarket.Api.Middleware
{
    public class ErrorhandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorhandlingMiddleware(RequestDelegate next, ILogger<ErrorhandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, System.Exception ex, ILogger logger)
        {
            HttpStatusCode code = ex switch
            {
                HttpBadRequestException _ => HttpStatusCode.BadRequest,
                HttpNotFoundException _ => HttpStatusCode.NotFound,
                HttpTooManyRequestsException _ => HttpStatusCode.TooManyRequests,
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            logger.LogError(ex, ex.Message);
            if (ex.InnerException != null)
            {
                logger.LogError(ex, ex.Message);
            }

            return context.Response.WriteAsync(result);
        }
    }
}
