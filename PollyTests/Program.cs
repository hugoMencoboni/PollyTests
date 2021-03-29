using Microsoft.Extensions.DependencyInjection;
using Polly.Services;
using PollyTests.Api;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Extensions.Http;

namespace PollyTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dependancyInjectionProvider = ConfigDependancies();
            var gitHubService = dependancyInjectionProvider.GetRequiredService<IGitHubService>();
            var outputService = dependancyInjectionProvider.GetRequiredService<IOutputService>();

            var user = await gitHubService.GetUserData("hugomencoboni");
            outputService.WriteLine(user);
        }

        private static ServiceProvider ConfigDependancies()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped<IGitHubService, GitHubService>()
                .AddScoped<IOutputService, OutputService>()
                .AddScoped<IGitHubApi, GitHubApi>();

            serviceCollection
                .AddHttpClient(GitHubApi.ClientName, GetGitHubConfiguration())
                .AddPolicyHandler(GetGitHubRetryPolicy());

            return serviceCollection.BuildServiceProvider();
        }

        private static Action<HttpClient> GetGitHubConfiguration()
        {
            return client =>
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryExample");
            };
        }

        static IAsyncPolicy<HttpResponseMessage> GetGitHubRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}

