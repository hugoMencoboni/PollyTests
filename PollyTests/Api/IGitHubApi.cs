using PollyTests.Api.Responses;
using System.Threading.Tasks;

namespace PollyTests.Api
{
    public interface IGitHubApi
    {
        Task<UserResponse> getUser(string userLogin);
    }
}