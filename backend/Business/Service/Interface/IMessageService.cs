using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IMessageService
    {
        Task<IEnumerable<IssueMessage>> GetMessagesAsync();
        Task<IssueMessage> GetMessageByIdAsync(int id);
        Task CreateMessageAsync(IssueMessageDto message);
        // Task UpdateMessageAsync(int id, MessageDto message);
        // Task DeleteMessageAsync(int id);
        Task<IEnumerable<IssueMessage>> GetMessagesbyIssueIdAsync(int issueId);
    }
}