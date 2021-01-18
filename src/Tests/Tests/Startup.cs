using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using poc_http_client.Application;
using Serilog.Extensions.Logging;
using Serilog;


namespace Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Environment.SetEnvironmentVariable("REDIS_BASE_URL", "localhost:6379");
            services.AddTransient<IBuilder, Builder>();

            services.AddLogging();


        }
    }
}