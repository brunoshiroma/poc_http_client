using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using poc_http_client.Models;


namespace poc_http_client.Application
{
    public class Post : RequestBase, IPost 
    {
        public Post(ILogger logger, HttpClient client) : base(client, logger)
        {}
        
        
        public Post Url(string url)
        {
            base.Url(url);
            return this;
        }

        public Post AddTimeout(uint ms)
        {
            
            base.AddTimeout(ms);
            return this;
        }
        
        public Post AddHeader(string key, string value="")
        {
            base.AddHeader(key, value);
            return this;
        }

        public Post Retry(int times)
        {
            base.Retry(times);
            return this;
        }

        public Post RetryAfterMs(uint ms)
        {
            base.RetryAfterMs(ms);
            return this;
        }
        
        public Post AddStringPayload(StringContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Post AddFormUrlEncoded(FormUrlEncodedContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Post AddFormData(MultipartFormDataContent payload)
        {
            base.AddBody(payload);
            return this;
        }
        
        public Post AddJson(JsonContent json)
        {
            base.AddBody(json);
            return this;
        }
        
        public Task<ResponseBase> Send()
        {
            base._method = "POST";
            Task<ResponseBase> result =  base.Send(); 
            return result;
        }

       
    }
}