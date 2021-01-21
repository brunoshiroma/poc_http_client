using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace poc_http_client.Application
{
    public interface IDelete
    {
        Task<ResponseBase> Send();
        Delete Url(string url);
        Delete AddTimeout(uint ms);
        Delete AddHeader(string key, string value);
        Delete Retry(int times);
        
        Delete RetryAfterMs(uint ms);
        Delete AddStringPayload(StringContent payload);
        Delete AddFormUrlEncoded(FormUrlEncodedContent payload);
        Delete AddFormData(MultipartFormDataContent payload);
        Delete AddJson(JsonContent json);
    }
}