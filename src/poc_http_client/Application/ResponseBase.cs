using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using poc_http_client.Infra;
using poc_http_client.Models;

namespace poc_http_client.Application
{
    public class ResponseBase
    {
        private readonly uint _statusCode;
        private readonly IEnumerator<KeyValuePair<string, IEnumerable<string>>> _headers;
        private readonly string _content ;
        private string _keyCache ;
        private Cache _cache;
        
        
        public ResponseBase(
            uint statusCode,
            IEnumerator<KeyValuePair<string, IEnumerable<string>>> headers,
            string content
        )
        {
            _statusCode = statusCode;
            _headers = headers;
            _content = content;
         }
        
        public ResponseBase(
            uint statusCode,
            IEnumerator<KeyValuePair<string, IEnumerable<string>>> headers,
            string content,
            string keyCache,
            Cache cache
            )
        {
            _statusCode = statusCode;
            _headers = headers;
            _content = content;
            _keyCache  = keyCache;
            _cache = cache;
        }

        public ResponseBase(
            uint? statusCode, 
            string message
            )
        {
            
            _statusCode = statusCode ?? 0 ;
            _content = message;
        }
        


        public T Content<T>()
        {
            return JsonConvert.DeserializeObject<T>(_content);
        }
        public string Content()
        {
            return _content;
        }
        public uint StatusCode()
        {
            return _statusCode;
        }
        public  IEnumerator<KeyValuePair<string, IEnumerable<string>>>  Headers()
        {
            return _headers;
        }

        public ResponseBase SaveCacheContent(TTLUnit unitTtl, double duration, string key)
        {
            if (!Equals(_cache, null))
            {
                _cache.Set(_content, key, duration, unitTtl);
            }

            return this;
        }
        
        public ResponseBase SaveCacheContent<T>(TTLUnit unitTtl, uint duration, string key)
        {
            
            if (!Equals(_cache, null))
            {
                string dataString = JsonConvert.DeserializeObject<T>(_content).ToString();
                _cache.Set(dataString, key, duration, unitTtl);
            }

            return this;
        }
    }
}