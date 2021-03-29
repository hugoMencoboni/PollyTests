using System;
using System.Text.Json;

namespace Polly.Services
{
    public class OutputService : IOutputService
    {
        public void WriteLine(object output)
        {
            Console.WriteLine(JsonSerializer.Serialize(output, new JsonSerializerOptions()
            {
                WriteIndented = true,
            }));
        }
    }
}
