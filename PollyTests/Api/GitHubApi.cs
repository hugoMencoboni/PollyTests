using PollyTests.Api.Responses;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PollyTests.Api
{
    public class GitHubApi : IGitHubApi
    {
        public static string ClientName = "GitHub";

        private readonly IHttpClientFactory _httpClientFactory;

        public GitHubApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserResponse> getUser(string userLogin)
        {
            var client = _httpClientFactory.CreateClient(ClientName);
            var response = await client.GetStringAsync($"/users/{userLogin}");
            return JsonSerializer.Deserialize<UserResponse>(response);
        }
    }
}
