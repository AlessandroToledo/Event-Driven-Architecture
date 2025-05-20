using ChatApp.Entities;
using ChatApp.Services.Interfaces;

namespace ChatApp.Services
{
    public class ChatService(IRedisService redisService) : IChatService
    {
        private readonly IRedisService _redisService = redisService;
        const string LOG_CHANNEL = "log-channel";
        const string OFFENSIVE_CHANNEL = "offensive-channel";
        public void HandleMessage(Message message)
        {
            _redisService.PublishEvent(LOG_CHANNEL, message);
            if(message.Offensive)
                _redisService.PublishEvent(OFFENSIVE_CHANNEL, message);
        }
    }
    
}
