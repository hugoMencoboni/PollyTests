using System.Text.Json.Serialization;

namespace PollyTests.Api.Responses
{
    public class UserResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }
        [JsonPropertyName("login")]
        public string Login { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; }
    }
}
