using FluentValidation;
using InvoiceSystem.API.ErrorsResponse;
//using System.ComponentModel.DataAnnotations;
using System.Net;

namespace InvoiceSystem.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (ValidationException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    statusCode = 400,
                    errors
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = _env.IsDevelopment()
                                   ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                   : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                await httpContext.Response.WriteAsJsonAsync(response);

            }

        }
    }
}
