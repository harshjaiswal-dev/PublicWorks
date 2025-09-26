using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private IActionTypeRepository? _actionTypeRepository;
        public IActionTypeRepository ActionTypeRepository => _actionTypeRepository ??= new ActionTypeRepository(_context);
        private IAuditTrailRepository? _auditTrailRepository;
        public IAuditTrailRepository AuditTrailRepository => _auditTrailRepository ??= new AuditTrailRepository(_context);
        private IImageRepository? _imageRepository;
        public IImageRepository ImageRepository => _imageRepository ??= new ImageRepository(_context);
        private IIssueRepository? _issueRepository;
        public IIssueRepository IssueRepository => _issueRepository ??= new IssueRepository(_context);
        private IMessageRepository? _messageRepository;
        public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_context);
        private IRemarkRepository? _remarkRepository;
        public IRemarkRepository RemarkRepository => _remarkRepository ??= new RemarkRepository(_context);
        private IUserRepository? _userRepository;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
     
    }
}