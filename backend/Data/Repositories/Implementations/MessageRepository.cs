using Data;
using Data.GenericRepository;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Implementations.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}