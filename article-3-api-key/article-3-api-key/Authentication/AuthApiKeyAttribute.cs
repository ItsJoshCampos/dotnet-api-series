using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace article_3_api_key.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthApiKeyAttribute : Attribute, IAsyncActionFilter
	{
        // API Key Header and ENV Name
        private const string ApiKeyHeader = "x-api-key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Look for the "x-api-key" Header in the request
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var apiKeyVal))
            {
                // If not found, throw a 401 status
                // 401 = Invalid or Missing Credentials
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("UNAUTHORIZED: API Key Missing");
            }

            // Get the real key from our ENV Variable
            var apiVal = Environment.GetEnvironmentVariable(ApiKeyHeader);
            if (!apiVal.Equals(apiKeyVal))
            {
                // If not found, throw a 401 status
                // 401 = Invalid or Missing Credentials
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("UNAUTHORIZED: Invalid API Key");
            }

            await next();
        }
    }
}

