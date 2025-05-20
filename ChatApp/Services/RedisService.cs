using ChatApp.Entities;
using ChatApp.Services.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace ChatApp.Services
{
    public class RedisService(string connectionString) : IRedisService
    {
        private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(connectionString);

        public async Task PublishEvent(string channel, Message message)
        {
            var json = JsonSerializer.Serialize(message);
            await _redis.GetSubscriber().PublishAsync(channel, json);
        }
    }
}
