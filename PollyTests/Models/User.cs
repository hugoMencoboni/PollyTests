namespace PollyTests.Models
{
    public record User
    {
        public int Id { get; init; }
        public string Login { get; init; }
        public string Name { get; init; }
    }
}
