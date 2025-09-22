using Business.Interface;
using Data;
using Data.GenericRepository;
using Data.Model;

namespace Business.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Message> Message{ get; private set; }
        public IGenericRepository<Status> Status { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Message = new GenericRepository<Message>(_context);
            Status = new GenericRepository<Status>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
