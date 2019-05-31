using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MVP.Middlewares
{
    public class OptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICorsService _corsService;
        private readonly ICorsPolicyProvider _corsPolicyProvider;
        private readonly string _corsPolicy;

        public OptionsMiddleware(RequestDelegate next, ICorsService corsService, ICorsPolicyProvider corsPolicyProvider, string corsPolicy)
        {
            _next = next;
            _corsService = corsService;
            _corsPolicyProvider = corsPolicyProvider;
            _corsPolicy = corsPolicy;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(CorsConstants.Origin))
            {
                var corsPolicy = await _corsPolicyProvider.GetPolicyAsync(context, "AllowAllHeaders");
                if (corsPolicy != null)
                {
                    var corsResult = _corsService.EvaluatePolicy(context, corsPolicy);
                    _corsService.ApplyResult(corsResult, context.Response);

                    if (context.Request.Method == HttpMethods.Options)
                    {
                        context.Response.StatusCode = StatusCodes.Status204NoContent;
                        return;
                    }
                }
            }

            await _next.Invoke(context);
        }
    }

}