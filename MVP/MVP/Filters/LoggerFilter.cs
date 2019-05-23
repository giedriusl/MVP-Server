using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MVP.BusinessLogic.Helpers.TokenGenerator;

namespace MVP.Filters
{
    public class LoggerFilter : TypeFilterAttribute
    {
        public LoggerFilter() : base(typeof(LoggerFilterImplementation))
        {
        }

        private class LoggerFilterImplementation : IActionFilter
        {
            private readonly ILogger _logger;

            public LoggerFilterImplementation(ILoggerFactory loggerFactory)
            {
                _logger = loggerFactory.CreateLogger<LoggerFilter>();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var role = string.Empty;
                var time = DateTimeOffset.Now;
                var user = context.HttpContext.User.Identity;
                string authHeader = context.HttpContext.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Bearer"))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    var parsedToken = JwtTokenGenerator.ParseToken(token);
                    var claims = parsedToken.Claims;
                    role = claims.First(claim => claim.Type == ClaimTypes.Role).Value;
                }

                var actionName = context.ActionDescriptor.DisplayName;

                var logMessage = $"User name: {user.Name}; Role: {role}; Method name: {actionName}; Time: {time}";

                _logger.Log(LogLevel.Information, logMessage);
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }

}
