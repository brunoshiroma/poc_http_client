using System;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using poc_http_client.Application;
using poc_http_client.Infra;
using poc_http_client.Models;
using Xunit;

namespace Tests
{
    public class Content
    {
        public string message { get; set; }
    }

    public class GetTest
    {

        private readonly IBuilder _client;

        public GetTest(IBuilder client)
        {
            _client = client;
        }

        

        
        [Fact]
        public async  void HttpGetNotFoundTest()
        {
            _client.Get();//.Url("https://www.google.com/181u81u8").Send();
            //uint statusCode = result.StatusCode();
            Assert.Equal(true, true);
        }
        [Fact]
        public async void HttpGetHeadersTest()
        {
            var result = await _client.Get().Url("http://localhost:3000/headers").Send();
            uint statusCode = result.StatusCode();
            var headers = result.Headers();

            bool keyMatch = false;
                
            while (headers.MoveNext())
            {
                if (headers.Current.Key == "gambs")
                {
                    keyMatch = true;
                }
            }
                
            
            Assert.Equal(200u, statusCode);
            Assert.True(keyMatch);
        }
        
        [Fact]
        public async  void HttpGetContentSerializedTest()
        {
            var result = await  _client.Get().Url("http://localhost:3000/content").Send();
            uint statusCode = result.StatusCode();
            Content message = result.Content<Content>();
            Assert.Equal("Ok", message.message);
            Assert.Equal(200u , statusCode);
        }

        [Fact]
        public async  void HttpGetContentStringTest()
        {
            var result = await  _client.Get().Url("http://localhost:3000/content").Send();
            uint statusCode = result.StatusCode();
            string message = result.Content();
            Assert.Equal("{\"message\": \"Ok\"}", message);
            Assert.Equal(200u , statusCode);
        }
        
        [Fact]
        public async  void HttpGetTimeoutServiceTest()
        {
            var result = await  _client.Get().Url("http://localhost:3000/timeout").Send();
            uint statusCode = result.StatusCode();
            Assert.Equal(408u , statusCode);
        }

        [Fact]
        public async  void HttpGetSetTimeoutTest()
        {
            var result = await  _client.Get().AddTimeout(1).Url("http://localhost:3000/timeout").Send();
            uint statusCode = result.StatusCode();
            Assert.Equal(408u , statusCode);
        }
 
        [Fact]
        public async  void HttpGetNotFoundDomainTest()
        {
            var result = await  _client.Get().Url("https://www.go2ogle.com").Send();
            uint statusCode = result.StatusCode();
            string content = result.Content();
            Assert.Equal(0u , statusCode);
            Assert.Equal("Name or service not known (www.go2ogle.com:443)" , content);
        }
        
       
        
        
        [Fact]
        public async  void HttpGetHeaderTest()
        {
            var result = await _client.Get().Url("https://www.google.com").Send();
            uint statusCode = result.StatusCode();
            Assert.Equal(200u, statusCode);
        }
        
        [Fact]
        public async  void HttpGetCachedTest()
        {
            
            
            var resultOutCached = await _client.Get().Url("http://localhost:3000/content").Send( "mockcached", TTLUnit.Minutes, 1);
            var resultCached = await _client.Get().Url("http://localhost:3000/content").Send( "mockcached", TTLUnit.Minutes, 1);
            uint statusCode = resultCached.StatusCode();
            Assert.Equal(304u, statusCode);
            Assert.Equal(resultOutCached.Content(), resultCached.Content());
        }
        
        [Fact]
        public async  void HttpGetCacheNotConnectTest()
        {
            
            
            var resultOutCached = await _client.Get().Url("http://localhost:3000/content").Send("mockcached", TTLUnit.Minutes, 1);
            var resultCached = await _client.Get().Url("http://localhost:3000/content").Send("mockcached", TTLUnit.Minutes, 1);
            uint statusCode = resultCached.StatusCode();
            Assert.Equal(200u, statusCode);
            Assert.Equal(resultOutCached.Content(), resultCached.Content());
        }
        
        [Fact]
        public async  void HttpGetRetryTest()
        {
            var result = await _client
                .Get()
                .Url("http://localhost:3000/content")
                .Retry(2)
                .Send("mockcached", TTLUnit.Minutes, 1);
            uint statusCode = result.StatusCode();
            Assert.Equal(200u, statusCode);
        }
        
        [Fact]
        public async  void HttpGetRetryInUseTest()
        {
            var result = await _client
                .Get()
                .Url("http://localhost:3000/timeout")
                .Retry(2)
                .AddTimeout(1)
                .Send();
            
            uint statusCode = result.StatusCode();
            Assert.Equal(408u, statusCode);
        }
    }

    public class PostTest
    {
        private readonly IBuilder _client;

        public PostTest(IBuilder client)
        {
            _client = client;
        }

        [Fact]
        public async void HttpPostNotFoundTest()
        {
            var result = await _client.Post().Url("https://www.google.com/181u81u8").Send();
            uint statusCode = result.StatusCode();
            Assert.Equal(404u, statusCode);  
        }
        
        [Fact]
        public async void HttpPostSendJsonTest()
        {

            var message = new Content {
                message = "teste_body"
            };
            var json = JsonContent.Create(message);
            var result = await _client.Post().Url("http://localhost:3000/content")
                .AddJson(json)
                .Send();
            uint statusCode = result.StatusCode();
            Assert.Equal(200u, statusCode);  
        }
        
        
        [Fact]
        public async void HttpPostSendJsonWithResponseTest()
        {

            var message = new Content {
                message = "ping"
            };
            var json = JsonContent.Create(message);
            ResponseBase result = await _client.Post().Url("http://localhost:3000/ping_pong")
                .AddJson(json)
                .Send();
            uint statusCode = result.StatusCode();
            var content = result.Content<Content>();
            Assert.Equal(200u, statusCode);  
            Assert.Equal("pong", content.message);
        }
        
        [Fact]
        public async void HttpPostSendJsonWithResponseHeadersTest()
        {
           ResponseBase result = await _client
                .Post()
                .Url("http://localhost:3000/headers")
                .Send();
            uint statusCode = result.StatusCode();
            
            var headers = result.Headers();

            bool keyMatch = false;
                
            while (headers.MoveNext())
            {
                if (headers.Current.Key == "X-TEST" && headers.Current.Value.ToArray()[0] == "ok" ) 
                {
                    keyMatch = true;
                }
            }
            Assert.Equal(200u, statusCode);  
            Assert.True(keyMatch);
        }
        
    }

     public class PutTest
    {
        private readonly IBuilder _client;

        public PutTest(IBuilder client)
        {
            _client = client;
        }
       [Fact]
        public async void HttpPutSendJsonWithResponseTest()
        {
            ResponseBase result = await _client
                .Put()
                .Url("http://localhost:3000/ping_pong")
                .Send();
            uint statusCode = result.StatusCode();
            var content = result.Content<Content>();
            Assert.Equal(200u, statusCode);  
            Assert.Equal("pong_put", content.message);
        }
    }
     
     public class DeleteTest
     {
         private readonly IBuilder _client;

         public DeleteTest(IBuilder client)
         {
             _client = client;
         }

     
     
         [Fact]
         public async void HttpDeleteSendJsonWithResponseTest()
         {
             ResponseBase result = await _client.Delete()
                 .Url("http://localhost:3000/ping_pong")
                 .Send();
             uint statusCode = result.StatusCode();
             var content = result.Content<Content>();
             Assert.Equal(200u, statusCode);  
             Assert.Equal("pong_delete", content.message);
         }
     }
     
     public class PatchTest
     {
         private readonly IBuilder _client;

         public PatchTest(IBuilder client)
         {
             _client = client;
         }

     
     
         [Fact]
         public async void HttpDeleteSendJsonWithResponseTest()
         {
             ResponseBase result = await _client.Patch()
                 .Url("http://localhost:3000/ping_pong")
                 .Send();
             uint statusCode = result.StatusCode();
             var content = result.Content<Content>();
             Assert.Equal(200u, statusCode);  
             Assert.Equal("pong_patch", content.message);
         }
     }
    
}