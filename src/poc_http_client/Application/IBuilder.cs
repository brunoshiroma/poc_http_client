namespace poc_http_client.Application
{
    public interface IBuilder
    {
        
        ResponseBase Default();
        Get Get();
        Post Post();
    }
}



