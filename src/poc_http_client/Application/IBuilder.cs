namespace poc_http_client.Application
{
    public interface IBuilder
    {
       
        Get Get();
        Post Post();
        Delete Delete();
        Patch Patch();
        Put Put();
    }
}



