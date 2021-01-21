using System;
using Polly.Retry;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;



namespace poc_http_client.Application
{
    public class RequestBase 
    {
        private string _url;
        private uint _timeout;
        private AsyncRetryPolicy _retryPolicy;
        protected string _method;
        private int _retry = 0;
        private uint _retryAfterMs = 1000; //default 
        private HttpContent _dataPayload;
        
        
        private ILogger _logger;
        private HttpClient _client;
        private IEnumerable<KeyValuePair<string, string>> _headers;

        public RequestBase(HttpClient client, ILogger logger)
        {
            _logger = logger;
            _client = client;
        }


        ///  <param name="ms">default 1000 ms</param>
        public RequestBase RetryAfterMs(uint ms)
        {
            _retryAfterMs= ms;
            return this;
        }
        public RequestBase Retry(int times)
        {
            _retry= times;
            return this;
        }
        
       protected RequestBase Url(string url)
        {
            _url = url;
            return this;
        }
       
       protected RequestBase AddBody(System.Net.Http.HttpContent payload)
       {
           _dataPayload = payload;
           return this;
       }
        protected RequestBase AddTimeout(uint ms)
        {
            _timeout = ms;
            return this;
        }
        protected RequestBase AddHeader(string key, string value="")
        {
            if (_headers.Any())
            {
                _headers = new List<KeyValuePair<string, string>>();
            }
            KeyValuePair<string, string> header = new KeyValuePair<string, string>(key, value);
            _headers = _headers. Append(header);
            return this;
        }

        public async Task<ResponseBase> Send()
        {
            try
            {
                
                if (_retry > 0)
                {      
                    _logger.LogInformation("Configurando retry tempo {0} quantidade de re tentativa{1}",_retryAfterMs, _retry );
                    _retryPolicy = new Infra.Retry().ConfigureRetry(_retryAfterMs, _retry);
                }
          
                HttpResponseMessage result = null;
                if (Equals(_retryPolicy, null))
                {
                    result = await internalSend();
                }
                else
                {

                    
                    result = 
                        await _retryPolicy.ExecuteAsync<HttpResponseMessage>(async () =>
                        {
                            _logger.LogInformation(" re tentativa");
                            return await internalSend();
                        });
                }
                return new ResponseBase(
                    (uint) result.StatusCode.GetHashCode(),
                    result.Headers.GetEnumerator(),
                    (await result.Content.ReadAsStringAsync())
                );
            }
            catch (System.Net.Http.HttpRequestException err)
            {
                _logger.LogError("Erro http {0}", err.Message);
                return new ResponseBase(
                    (uint) err.StatusCode.GetHashCode(),
                    err.Message
                );
            }
            // timeout default 100 seconds
            catch (System.Threading.Tasks.TaskCanceledException err)
            {
                _logger.LogError("Timeout ms time configurado {1} : {0} ", err.Message, _timeout != 0  ?  10000: _timeout );
                return new ResponseBase(
                    408u, 
                    err.Message
                );  
            }
            
        }


        private async Task<HttpResponseMessage> internalSend()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = new HttpMethod(_method);
            requestMessage.RequestUri = new Uri(_url);
            
            if (!Equals(_dataPayload, null) && _method !="GET")
            {
                requestMessage.Content = _dataPayload;
            }

            if (!Equals(null,_headers))
            {
                foreach (var header in _headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            var _client = new HttpClient();
                
            if (_timeout != 0)
            {
                _client.Timeout =  TimeSpan.FromMilliseconds(_timeout) ;
            }
            
            HttpResponseMessage result = await _client.SendAsync(requestMessage);
            return result;
        }
    }        
}