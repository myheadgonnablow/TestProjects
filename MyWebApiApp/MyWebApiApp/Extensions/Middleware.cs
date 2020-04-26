using Microsoft.AspNetCore.Builder;

namespace MyWebApiApp.Middleware
{
    public static class Middleware
    {
        public static IApplicationBuilder UseCustomExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }

        public static IApplicationBuilder UseThrowsBeforeNextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThrowsBeforeNext>();
        }

        public static IApplicationBuilder UseThrowsAfterNextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThrowsAfterNext>();
        }
    }
}
