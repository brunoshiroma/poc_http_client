using System;
using System.Net.Http.Json;

namespace poc_http_client.Models
{
    public enum PayloadType
    {
        STRING,
        JSON,
        FORM_DATA,
        FORM_URL_ENCODED
    }

    public class PayloadData
    {
        public T Data<T>(T d) where T: System.Net.Http.HttpContent
        {


            return d;

        }
    }


}