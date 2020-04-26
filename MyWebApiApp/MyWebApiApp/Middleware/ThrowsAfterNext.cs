using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyWebApiApp.Middleware
{
    public class ThrowsAfterNext : IMiddleware
    {
        Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            next(context);
            throw new System.NotImplementedException("ThrowsAfterNext");
        }
    }
}
