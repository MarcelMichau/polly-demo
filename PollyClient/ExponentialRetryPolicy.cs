using System;
using System.Net.Http;
using Polly;
using Polly.Retry;

namespace PollyClient;

public static class ExponentialRetryPolicy
{
    public static AsyncRetryPolicy GetPolicy() => Policy
        .Handle<HttpRequestException>()
        .WaitAndRetryAsync(5, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (exception, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"Retrying HTTP call, attempt: {retryCount}, waiting {timeSpan.Seconds} seconds");
            }
        );
}