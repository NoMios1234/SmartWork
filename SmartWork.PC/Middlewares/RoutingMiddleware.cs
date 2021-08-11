using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using System.Threading.Tasks;

namespace SmartWork.PC.Middlewares
{
    public class RoutingMiddleware
    {
        private readonly RequestDelegate _next;

        public RoutingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var url = context.Request.GetDisplayUrl();
            Log.Information(url);
            await _next.Invoke(context);
        }
    }
}
