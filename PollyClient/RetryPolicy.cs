using System;
using System.Net.Http;
using Polly;
using Polly.Retry;

namespace PollyClient;

public static class RetryPolicy
{
    public static AsyncRetryPolicy GetPolicy() => Policy.Handle<HttpRequestException>().RetryAsync(3,
        (exception, attempt) => { Console.WriteLine($"Retrying HTTP call, attempt: {attempt}"); });
}