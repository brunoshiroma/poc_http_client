using System.Threading.Tasks;
using poc_http_client.Models;

namespace poc_http_client.Application
{
    public interface IGet
    {
        Task<ResponseBase> Send(string key, TTLUnit ttlUnit, double time);
        Task<ResponseBase> Send();
        Get Url(string url);
        Get AddTimeout(uint ms);
        Get AddHeader(string key, string value);
        Get Retry(int times);
        RequestBase RetryAfterMs(uint ms);
    }
}