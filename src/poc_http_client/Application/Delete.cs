using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using poc_http_client.Models;


namespace poc_http_client.Application
{
    public class Delete : RequestBase, IDelete 
    {
        public Delete(ILogger logger, HttpClient client) : base(client, logger)
        {}
        
        
        public Delete Url(string url)
        {
            base.Url(url);
            return this;
        }

        public Delete AddTimeout(uint ms)
        {
            
            base.AddTimeout(ms);
            return this;
        }
        
        public Delete AddHeader(string key, string value="")
        {
            base.AddHeader(key, value);
            return this;
        }

        public Delete Retry(int times)
        {
            base.Retry(times);
            return this;
        }

        public Delete RetryAfterMs(uint ms)
        {
            base.RetryAfterMs(ms);
            return this;
        }
        
        public Delete AddStringPayload(StringContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Delete AddFormUrlEncoded(FormUrlEncodedContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Delete AddFormData(MultipartFormDataContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Delete AddJson(JsonContent json)
        {
            base.AddBody(json);
            return this;
        }
        
        public Task<ResponseBase> Send()
        {
            base._method = "DELETE";
            Task<ResponseBase> result =  base.Send(); 
            return result;
        }

       
    }
}