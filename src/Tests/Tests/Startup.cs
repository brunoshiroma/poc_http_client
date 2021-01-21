using System;
using Microsoft.Extensions.DependencyInjection;
using poc_http_client.Application;


namespace Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Environment.SetEnvironmentVariable("REDIS_BASE_URL", "localhost:6379");
            //services.AddTransient<IBuilder, Builder>();
            services.AddHttpClient<IBuilder, Builder>();
            services.AddLogging();
            


        }
    }
}