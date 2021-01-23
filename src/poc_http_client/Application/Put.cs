using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using poc_http_client.Models;


namespace poc_http_client.Application
{
    public class Put : RequestBase, IPut 
    {
        public Put(ILogger logger, HttpClient client) : base(client, logger)
        {}
        
        
        public Put Url(string url)
        {
            base.Url(url);
            return this;
        }

        public Put AddTimeout(uint ms)
        {
            
            base.AddTimeout(ms);
            return this;
        }
        
        public Put AddHeader(string key, string value="")
        {
            base.AddHeader(key, value);
            return this;
        }

        public Put Retry(int times)
        {
            base.Retry(times);
            return this;
        }

        public Put RetryAfterMs(uint ms)
        {
            base.RetryAfterMs(ms);
            return this;
        }
        
        public Put AddStringPayload(StringContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Put AddFormUrlEncoded(FormUrlEncodedContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Put AddFormData(MultipartFormDataContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Put AddJson(JsonContent json)
        {
            base.AddBody(json);
            return this;
        }
        
        public Task<ResponseBase> Send()
        {
            base._method = "PUT";
            Task<ResponseBase> result =  base.Send(); 
            return result;
        }

       
    }
}