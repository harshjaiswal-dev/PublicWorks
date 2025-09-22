using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesAsync();
        Task<Message> GetMessageByIdAsync(int id);
        Task CreateMessageAsync(MessageDto message);
        Task UpdateMessageAsync(int id, MessageDto message);
        Task DeleteMessageAsync(int id);
    }
}