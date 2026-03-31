namespace OctoBridge.Infrastructure.Extensions;

using Polly;
using System.Net;
using Polly.Extensions.Http;
using OctoBridge.Domain.Constants;

public static class HttpClientPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                AppConstants.MaxRetryAttempts,
                retry => TimeSpan.FromSeconds(Math.Pow(AppConstants.RetryDelayInSecond, retry)),
                (result, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} after {timeSpan}");
                }
            );
    }

    public static IAsyncPolicy<HttpResponseMessage> RateLimitPolicy()
    {
        return Policy
            .HandleResult<HttpResponseMessage>(r =>
                r.StatusCode == HttpStatusCode.TooManyRequests ||
                r.StatusCode == HttpStatusCode.ServiceUnavailable)
            .WaitAndRetryAsync(AppConstants.MaxRateLimit, _ => TimeSpan.FromSeconds(AppConstants.RateLimitIntervalInSeconds));
    }
}

