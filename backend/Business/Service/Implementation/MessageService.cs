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
            // Validate sender exists
            var senderExists = await _unitOfWork.UserRepository.ExistsAsync(dto.SenderId);
            if (!senderExists)
                throw new Exception($"Sender with ID {dto.SenderId} does not exist.");

            // Validate recipient exists
            var recipientExists = await _unitOfWork.UserRepository.ExistsAsync(dto.RecipientId);
            if (!recipientExists)
                throw new Exception($"Recipient with ID {dto.RecipientId} does not exist.");
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
            // await _unitOfWork.SaveAsync();      
        }

        public async Task<IEnumerable<IssueMessage>> GetMessagesbyIssueIdAsync(int issueId)
        {
            return await _unitOfWork.IssueMessageRepository.GetMessagesbyIssueIdAsync(issueId);
        }

        // public async Task UpdateMessageAsync(int id, MessageDto dto)
        // {
        //     var message = new Message()
        //     {
        //         ID = dto.ID,
        //         IssueId = dto.IssueId,
        //         SendBy = dto.SendBy,
        //         SendTo = dto.SendTo,
        //         Subject = dto.Subject,
        //         Body = dto.Body,
        //         SendDate = dto.SendDate
        //     };

        //     await _unitOfWork.MessageRepository.UpdateAsync(id, message);
        //     await _unitOfWork.SaveAsync();
        // }

        // public async Task DeleteMessageAsync(int id)
        // {
        //     await _unitOfWork.MessageRepository.DeleteAsync(id);
        //     await _unitOfWork.SaveAsync();
        // }
    }
}