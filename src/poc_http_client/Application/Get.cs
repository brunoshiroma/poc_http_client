using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using poc_http_client.Infra;
using poc_http_client.Models;


namespace poc_http_client.Application
{
    public class Get : RequestBase , IGet
    {
        private Cache _cache;
        private ILogger _logger;
        public Get(Cache cache, ILogger logger, HttpClient client) : base(client, logger)
        {
            _logger = logger;
            _cache = cache;
        }
        /// <summary>
        /// Definicao da url que sera chamada
        /// </summary>
        /// <param name="url"></param>
        public Get Url(string url)
        {
            base.Url(url);
            return this;
        }
        /// <summary>
        /// Adicao do tempo de timeout
        /// </summary>
        /// <param name="ms">default 10000</param>
        public Get AddTimeout(uint ms)
        {
            
            base.AddTimeout(ms);
            return this;
        }
        
        
        /// <summary>
        /// Adicao de headers
        /// </summary>
        /// <param name="key">chave do header</param>
        /// <param name="value">valor</param>
        public Get AddHeader(string key, string value="")
        {
            base.AddHeader(key, value);
            return this;
        }
        /// <summary>
        /// Adicao de re tentativas
        /// </summary>
        /// <param name="times"></param>
        public Get Retry(int times)
        {
            base.Retry(times);
            return this;
        }
        
        /// <summary>
        /// Adicao do tempo a se esperar ate re tentar
        /// </summary>
        public Get RetryAfterMs(uint ms)
        {
            base.RetryAfterMs(ms);
            return this;
        }
        
        /// <summary>
        /// Envia a solicitacao sem utilizar o cache
        /// </summary>
        public async  Task<ResponseBase> Send()
        {
            using (_logger.BeginScope("GET"))
            { 
                base._method = "GET";
                return await base.Send();
            }
        }
        
        /// <summary>
        /// Envia a solicitacao usando o cache
        /// </summary>
        /// <param name="key">chave que sera utilizada para salvar o cache ou buscada</param>
        /// <param name="ttlUnit">unidade de tempo para expirar o cache</param>
        /// <param name="duration">valor da duracao </param>
        public async  Task<ResponseBase> Send(string key, TTLUnit ttlUnit, double duration)
        {
            using (_logger.BeginScope("GET com cache"))
            {
                
                base._method = "GET";
                string dataCached = await _cache.Get(key);
                ResponseBase result;
                if (!String.IsNullOrEmpty(dataCached))
                {
                    _logger.LogInformation("Recuperado cache com sucesso");
                    result = new ResponseBase(304u, dataCached);
                }
                else
                {
                    result = await base.Send();
                    if (result.StatusCode() == 200)
                    {
                        _logger.LogInformation("Salvando cache chave {0} duracao {1} {2}", key, duration, ttlUnit.ToString());
                        await _cache.Set(result.Content(),key, duration, ttlUnit );
                    }
                }
                return result;
            }
        }
    }
}