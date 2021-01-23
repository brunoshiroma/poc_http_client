using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using poc_http_client.Models;

namespace poc_http_client.Application
{
    public interface IPost
    {
        Task<ResponseBase> Send();
        Post Url(string url);
        Post AddTimeout(uint ms);
        Post AddHeader(string key, string value);
        Post Retry(int times);
        Post RetryAfterMs(uint ms);
        public Post AddStringPayload(StringContent payload);
        public Post AddFormUrlEncoded(FormUrlEncodedContent payload);
        public Post AddFormData(MultipartFormDataContent payload);
        public Post AddJson<T>(T body);
    }
}