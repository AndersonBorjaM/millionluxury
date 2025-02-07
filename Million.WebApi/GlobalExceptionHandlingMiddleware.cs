using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Million.WebApplication
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException || ex is UnauthorizedAccessException)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (InvalidOperationException ex)
            { 
                await HandleExceptionAsync(context, ex, HttpStatusCode.ExpectationFailed, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Ha ocurrido un erro por favor intentar mas tarde.");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Details = exception.StackTrace
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
