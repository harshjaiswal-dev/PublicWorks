using Data.Interfaces;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Implementations.Repositories;

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
        private IStatusRepository? _statusRepository;
        public IStatusRepository StatusRepository => _statusRepository ??= new StatusRepository(_context);
       private ICategoryRepository? _categoryRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
         private IRoleRepository? _roleRepository;
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_context);
        private IPriorityRepository? _priorityRepository;
        public IPriorityRepository PriorityRepository => _priorityRepository ??= new PriorityRepository(_context);
       
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