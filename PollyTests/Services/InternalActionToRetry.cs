using System.Threading.Tasks;
using Polly.Retry;
using System;

namespace Polly.Services
{
    public class InternalActionToRetry
    {
        private static readonly Random Random = new Random();
        private readonly AsyncRetryPolicy _asyncRetryPolicy;

        public InternalActionToRetry()
        {
            _asyncRetryPolicy = Policy.Handle<HandleByRetryPollicy>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(2, retryAttempt)));
        }

        public async Task<string> RetriedAction()
        {
            return await _asyncRetryPolicy.ExecuteAsync(async () => await Action());
        }

        public Task<string> Action()
        {
            var randomNumber = Random.Next(1, 4);
            switch(randomNumber)
            {
                case 1:
                    throw new HandleByRetryPollicy();
                case 2:
                    throw new NotHandleByRetryPollicy();
                case 3:
                default:
                    return Task.FromResult("azertyuio");
            }
        }
    }

    public class HandleByRetryPollicy : Exception
    {
        public HandleByRetryPollicy() : base("Handle") { }
    }

    public class NotHandleByRetryPollicy : Exception
    {
        public NotHandleByRetryPollicy() : base("Not handle") { }
    }
}
