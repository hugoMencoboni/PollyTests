using PollyTests.Api.Responses;
using PollyTests.Models;

namespace PollyTests.Mapper
{
    public static class GitHubUserResponseToUser
    {
        public static User CreateUser(this UserResponse gitHubUserResponse)
        {
            return new User()
            {
                Id = gitHubUserResponse.Id,
                Login = gitHubUserResponse.Login,
                Name = gitHubUserResponse.Name,
            };
        }
    }
}
