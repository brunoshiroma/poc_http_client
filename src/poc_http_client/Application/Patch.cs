using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using poc_http_client.Models;


namespace poc_http_client.Application
{
    public class Patch : RequestBase, IPatch 
    {
        public Patch(ILogger logger, HttpClient client) : base(client, logger)
        {}
        
        
        public Patch Url(string url)
        {
            base.Url(url);
            return this;
        }

        public Patch AddTimeout(uint ms)
        {
            
            base.AddTimeout(ms);
            return this;
        }
        
        public Patch AddHeader(string key, string value="")
        {
            base.AddHeader(key, value);
            return this;
        }

        public Patch Retry(int times)
        {
            base.Retry(times);
            return this;
        }

        public Patch RetryAfterMs(uint ms)
        {
            base.RetryAfterMs(ms);
            return this;
        }
        
        public Patch AddStringPayload(StringContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Patch AddFormUrlEncoded(FormUrlEncodedContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Patch AddFormData(MultipartFormDataContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Patch AddJson(JsonContent json)
        {
            base.AddBody(json);
            return this;
        }
        
        public Task<ResponseBase> Send()
        {
            base._method = "Patch";
            Task<ResponseBase> result =  base.Send(); 
            return result;
        }

       
    }
}