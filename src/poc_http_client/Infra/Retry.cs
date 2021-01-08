using System;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;

namespace poc_http_client.Infra
{
    public class Retry
    {
      

        public AsyncRetryPolicy ConfigureRetry(uint _retryAfterMs, int _retry)
        {
            return Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(_retry, retryAttempt => {
                     
                    TimeSpan timeToWait = TimeSpan.FromMilliseconds(_retryAfterMs);
                    // colocar log 
                    Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                    return timeToWait;
                });
        }

        public async Task<T> ExecuteAsync<T>(AsyncRetryPolicy policy, Func<Task<T>> fallback)
        {
            return await policy.ExecuteAsync<T>(fallback);
        }
    }
}