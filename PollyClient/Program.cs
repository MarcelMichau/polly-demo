using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Starting program execution...");

            var client = new HttpClient();

            var policy = ExponentialRetryPolicy.GetPolicy();

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync("https://localhost:5001/api/weatherforecast");

                    Console.WriteLine($"Weather Forecast: {await response.Content.ReadAsStringAsync()}");
                });
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Oh no! The call failed.");
            }
        }
    }
}