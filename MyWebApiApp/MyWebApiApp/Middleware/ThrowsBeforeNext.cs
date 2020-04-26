using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyWebApiApp.Middleware
{
    public class ThrowsBeforeNext : IMiddleware
    {
        Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new System.NotImplementedException("ThrowsBeforeNext");
            return next(context);
        }
    }
}
