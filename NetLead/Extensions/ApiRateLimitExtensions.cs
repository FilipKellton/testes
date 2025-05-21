using Microsoft.Net.Http.Headers;
using System.Net;
using System.Threading.RateLimiting;

namespace NetLead.Extensions;

public static class ApiRateLimitExtensions
{
    public static void AddRateLimit(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 1,
                        QueueLimit = 1,
                        Window = TimeSpan.FromMinutes(3),
                    }));
            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                var retryAfterSeconds = TimeSpan.FromMinutes(3).TotalSeconds;

                context.HttpContext.Response.Headers[HeaderNames.RetryAfter] = retryAfterSeconds.ToString();

                await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", cancellationToken: token);
            };
        });
    }
}
