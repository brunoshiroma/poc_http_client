using System;
using poc_http_client.Application;

namespace poc_http_client.Infra
{
    public static class EnvironmentVariables
    {
        public static string _REDIS_BASE_URL = Environment.GetEnvironmentVariable("REDIS_BASE_URL") ;
    }
}