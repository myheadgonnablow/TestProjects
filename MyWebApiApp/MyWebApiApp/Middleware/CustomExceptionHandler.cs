using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyWebApiApp.Extensions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MyWebApiApp.Middleware
{
    public class CustomExceptionHandler : IMiddleware
    {
        private static readonly int DefaultErrorStatusCode = (int)HttpStatusCode.InternalServerError;

        ILogger _logger;
        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (context.Response.HasStarted)
            {
                return Task.CompletedTask;
            }

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = DefaultErrorStatusCode;

            // Could be some real CustomErrorResponse instead of anonymous;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                StatusCode = DefaultErrorStatusCode,
                Message = "Client-friendly error message",
            }));
        }
    }
}
