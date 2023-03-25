using System;
namespace article_3_api_key.Authentication
{
	public class AuthApiKeyMiddleware
	{
		// Request Delete that is used to manage each HTTP request.
		private readonly RequestDelegate _next;
		// API Key Header and ENV Name
		private const string ApiKeyHeader = "x-api-key";

		// Inject Request Delegate into API Key Middleware
		public AuthApiKeyMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		// Pass the Request's HTTP Context
		public async Task InvokeAsync(HttpContext context)
		{
			// Look for the "x-api-key" Header in the request
			if(!context.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedApiKey))
			{
				// If not found, throw a 401 status
				// 401 = Invalid or Missing Credentials
				// https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401
				context.Response.StatusCode = 401;
				await context.Response.WriteAsync("UNAUTHORIZED: API Key Missing");
				return;
			}

			// Get the real key from our ENV Variable
            var apiVal = Environment.GetEnvironmentVariable(ApiKeyHeader);

			// API Key found in Header
			// Validate the Key
			if(!apiVal.Equals(extractedApiKey))
			{
                // If key is not valid, throw a 401 status
                // 401 = Invalid or Missing Credentials
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401
                context.Response.StatusCode = 401;
				await context.Response.WriteAsync("UNAUTHORIZED: Invalid API Key");
			}

			// Valid API Key was provided
			// Pass request to the next delegate in the pipeline
			await _next(context);
		}
	}
}

