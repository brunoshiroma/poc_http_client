using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using poc_http_client.Infra;
using poc_http_client.Models;

namespace poc_http_client.Application
{
    public class Get : RequestBase , IGet
    {
        private Cache _cache;
        
        public Get(Cache cache, ILogger logger, HttpClient client) : base(client, logger)
        {
            _cache = cache;
        }

        public Get Url(string url)
        {
            base.Url(url);
            return this;
        }

        public Get AddTimeout(uint ms)
        {
            
            base.AddTimeout(ms);
            return this;
        }
        
        public Get AddHeader(string key, string value="")
        {
            base.AddHeader(key, value);
            return this;
        }

        public Get Retry(int times)
        {
            base.Retry(times);
            return this;
        }
        
        ///  <param name="ms">default 1000 ms</param>
        public RequestBase RetryAfterMs(uint ms)
        {
            base.RetryAfterMs(ms);
            return this;
        }

        public async  Task<ResponseBase> Send()
        {
           base._method = "GET";
           return await base.Send();

        }
        
        public async  Task<ResponseBase> Send(bool useCache, string key, TTLUnit ttlUnit, double time)
        {
            base._method = "GET";
            string dataCached = await _cache.Get(key);
            ResponseBase result;
            if (!String.IsNullOrEmpty(dataCached))
            {
                result =  new ResponseBase(304u, dataCached);
            }
            else
            {
                result = await base.Send();
                if (result.StatusCode() == 200)
                {
                    await _cache.Set(result.Content(),key, time, ttlUnit );
                }
            }
            return result;
        }

        
     
    }
}