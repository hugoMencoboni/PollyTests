using PollyTests.Api;
using PollyTests.Mapper;
using PollyTests.Models;
using System.Threading.Tasks;

namespace Polly.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubApi _gitHubApi;

        public GitHubService(IGitHubApi gitHubApi)
        {
            _gitHubApi = gitHubApi;
        }

        public async Task<User> GetUserData(string userLogin)
        {
            var userResponse = await _gitHubApi.getUser(userLogin);
            return userResponse.CreateUser();
        }
    }
}
