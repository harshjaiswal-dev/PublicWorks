using Data.GenericRepository;
using Data.Model;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
            
        }
         public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId)
    {
        return await _context.Message
            .Where(m => m.SendBy == userId || m.SendTo == userId)
            .ToListAsync();
    }
    }
}