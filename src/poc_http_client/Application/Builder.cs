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
        
        public Builder(ILogger<Builder> logger)
        {
            _cache = new Cache(EnvironmentVariables._REDIS_BASE_URL);
            _logger = logger;
            _client = new HttpClient();
        }
        
        public Builder(ILogger<Builder> logger, HttpClient httpClient)
        {
            _cache = new Cache(EnvironmentVariables._REDIS_BASE_URL);
            _logger = logger;
            _client = httpClient;
        }
        

        public Get Get()
        {
            return new Get(_cache, _logger, _client);
        }
        
        public Post Post()
        {
            return new Post( _logger, _client);
        }
        
        public Delete Delete()
        {
            return new Delete( _logger, _client);
        }
        
        public Patch Patch()
        {
            return new Patch( _logger, _client);
        }
        
        public Put Put()
        {
            return new Put( _logger, _client);
        }
        
        
    }
}