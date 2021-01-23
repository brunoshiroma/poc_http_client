using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using poc_http_client.Models;

namespace poc_http_client.Application
{
    public interface IPut
    {
        Task<ResponseBase> Send();
        Put Url(string url);
        Put AddTimeout(uint ms);
        Put AddHeader(string key, string value);
        Put Retry(int times);
        
        Put RetryAfterMs(uint ms);
        public Put AddStringPayload(StringContent payload);
        public Put AddFormUrlEncoded(FormUrlEncodedContent payload);
        public Put AddFormData(MultipartFormDataContent payload);
        public Put AddJson(JsonContent json);




    }
}