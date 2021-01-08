using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using poc_http_client.Infra;

namespace poc_http_client.Application
{
    public class Builder : IBuilder
    {
        private Cache _cache;
        private ILogger _logger;
        private HttpClient _client;
        
        public Builder(){}
        public Builder(ILogger<Builder> logger)
        {
            //_cache = new Cache(EnvironmentVariables._REDIS_BASE_URL);
            _logger = logger;
            //_client = new HttpClient();
        }
        
     
        
        
        public ResponseBase Default()
        {
            throw new System.NotImplementedException();
        }

        public void Get()
        {
           // return new Get(_cache, _logger, _client);
        }
    }
}