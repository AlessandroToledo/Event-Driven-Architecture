using ChatApp.Entities;

namespace ChatApp.Services.Interfaces
{
    public interface IRedisService
    {
        Task PublishEvent(string channel, Message message);
    }
}
