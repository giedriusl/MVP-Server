using Microsoft.AspNetCore.Builder;

namespace MVP.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseOptions(this IApplicationBuilder builder, string policy)
        {
            return builder.UseMiddleware<OptionsMiddleware>(policy);
        }
    }
}
