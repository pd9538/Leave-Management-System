using System.Net;
using System.Text.Json;
using leave_management_api.Models;

namespace leave_management_api.Middleware
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
                await _next(context);
            }

            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;

                var response = new ErrorResponse
                {
                    StatusCode = context.Response.StatusCode,

                    Message = ex.Message
                };

                var json =
                    JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}