using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<IssueMessage>> GetMessagesAsync()
        {
            return await _unitOfWork.IssueMessageRepository.GetAllAsync();
        }

        public async Task<IssueMessage> GetMessageByIdAsync(int id)
        {
            return await _unitOfWork.IssueMessageRepository.GetByIdAsync(id);
        }

        public async Task CreateMessageAsync(IssueMessageDto dto)
        {
            var message = new IssueMessage()
            {
                MessageId = dto.MessageId,
                IssueId = dto.IssueId,
                SentByUserId = dto.SenderId,
                SentToUserId = dto.RecipientId,
                Subject = dto.Subject,
                Body = dto.Body,
                SentAt = dto.SentAt
            };

            await _unitOfWork.IssueMessageRepository.AddAsync(message);
            await _unitOfWork.SaveAsync();
        }

    }
}