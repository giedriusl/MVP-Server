using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MVP.Filters
{
    public class LoggerFilter : TypeFilterAttribute
    {
        public LoggerFilter() : base(typeof(LoggerFilterImplementation))
        {
        }

        private class LoggerFilterImplementation : IResultFilter
        {
            private readonly ILogger _logger;

            public LoggerFilterImplementation(ILoggerFactory loggerFactory)
            {
                _logger = loggerFactory.CreateLogger<LoggerFilter>();
            }

            public void OnResultExecuting(ResultExecutingContext context)
            {
                var user = context.HttpContext.User.Identity;
                _logger.Log(LogLevel.Information, $"Methods...{user.Name}");
            }

            public void OnResultExecuted(ResultExecutedContext context)
            {
            }
        }
    }
    
}
