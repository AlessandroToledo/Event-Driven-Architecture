using ChatApp.Entities;

namespace ChatApp.Services.Interfaces
{
    public interface IChatService
    {
        void HandleMessage(Message message);
    }
}
