using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IUoW _unitOfWork;

        public MessageService(IUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _unitOfWork.MessageRepository.GetAllAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _unitOfWork.MessageRepository.GetByIdAsync(id);
        }

        public async Task CreateMessageAsync(MessageDto dto)
        {
            var message = new Message()
            {
                ID = dto.ID,
                IssueId = dto.IssueId,
                SendBy = dto.SendBy,
                SendTo = dto.SendTo,
                Subject = dto.Subject,
                Body = dto.Body,
                SendDate = dto.SendDate
            };

            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateMessageAsync(int id, MessageDto dto)
        {
            var message = new Message()
            {
                ID = dto.ID,
                IssueId = dto.IssueId,
                SendBy = dto.SendBy,
                SendTo = dto.SendTo,
                Subject = dto.Subject,
                Body = dto.Body,
                SendDate = dto.SendDate
            };

            await _unitOfWork.MessageRepository.UpdateAsync(id, message);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteMessageAsync(int id)
        {
            await _unitOfWork.MessageRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}