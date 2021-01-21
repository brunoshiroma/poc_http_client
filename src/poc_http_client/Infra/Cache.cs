using System;
using System.Threading.Tasks;
using poc_http_client.Models;
using StackExchange.Redis;

namespace poc_http_client.Infra
{
    public class Cache
    {
        private ConnectionMultiplexer _connection;

        public Cache(string redisBaseUrl)
        {
            if (!String.IsNullOrEmpty(redisBaseUrl))
            {
                _connection = ConnectionMultiplexer.Connect(redisBaseUrl);
            }
        }
       
        private TimeSpan timeSpanForTTLUnit(double time, TTLUnit ttlUnit)
        {

            switch (ttlUnit)
            {
                case TTLUnit.Hours:
                    return TimeSpan.FromHours(time);
                case TTLUnit.Minutes:
                    return TimeSpan.FromMinutes(time);
                case TTLUnit.Seconds:
                    return TimeSpan.FromSeconds(time);
                case TTLUnit.MilliSeconds:
                    return TimeSpan.FromMilliseconds(time);
                default:
                    return TimeSpan.FromMilliseconds(time);
            }
            
        }


        public async Task<string> Get(string key)
        {
            if (!Equals(_connection, null) && _connection.IsConnected)
            {
                var dbRedis = _connection.GetDatabase();
                string result = await dbRedis.StringGetAsync(key);
                return result;    
            }
            return String.Empty;
            
        }

        public  Task Set(string data, string key, double time, TTLUnit ttlUnit)
        {
            if (!Equals(_connection, null) && _connection.IsConnected)
            {
                var dbRedis = _connection.GetDatabase();
                //ver se preciso colcar o wait 
                TimeSpan _ttl = timeSpanForTTLUnit(time, ttlUnit);
                dbRedis.StringSetAsync(key, data, _ttl);    
            }

            
            return Task.CompletedTask;
            
        }
    }
}