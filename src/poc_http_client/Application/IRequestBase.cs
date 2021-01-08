namespace poc_http_client.Application
{
    public interface IRequestBase
    {
        public RequestBase Url(string url);
        protected RequestBase AddTimeout(uint ms);
        protected RequestBase AddHeader(string key, string value = "");
    }
}