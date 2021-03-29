using PollyTests.Models;
using System.Threading.Tasks;

namespace Polly.Services
{
    public interface IGitHubService
    {
        public Task<User> GetUserData(string userLogin);
    }
}
