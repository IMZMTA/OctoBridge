namespace OctoBridge.Domain.Constants;

public static class ResilienceConstants
{
    public const int MaxRetryAttempts = 3;
    public const int RetryDelaySeconds = 1;

    public const int MaxRateLimit = 2;
    public const int RateLimitIntervalSeconds = 5;
}