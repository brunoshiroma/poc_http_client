using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace poc_http_client.Application
{
    public interface IPatch
    {
        Task<ResponseBase> Send();
        Patch Url(string url);
        Patch AddTimeout(uint ms);
        Patch AddHeader(string key, string value);
        Patch Retry(int times);
        
        Patch RetryAfterMs(uint ms);
        Patch AddStringPayload(StringContent payload);
        Patch AddFormUrlEncoded(FormUrlEncodedContent payload);
        Patch AddFormData(MultipartFormDataContent payload);
        Patch AddJson(JsonContent json);
    }
}