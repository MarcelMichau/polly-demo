using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyClient
{
    internal class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Starting program execution...");

            var policy = ExponentialRetryPolicy.GetPolicy();

            try
            {
                await policy.ExecuteAsync(CallApi);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Oh no! The call failed.");
            }
        }

        private static async Task CallApi()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("https://localhost:5001/api/weatherforecast");

            Console.WriteLine($"Weather Forecast: {await response.Content.ReadAsStringAsync()}");
        }
    }
}