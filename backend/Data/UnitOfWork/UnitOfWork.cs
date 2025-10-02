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
        private IIssueStatusRepository? _issueStatusRepository;
        public IIssueStatusRepository IssueStatusRepository => _issueStatusRepository ??= new IssueStatusRepository(_context);
        private IIssueCategoryRepository? _issueCategoryRepository;
        public IIssueCategoryRepository IssueCategoryRepository => _issueCategoryRepository ??= new IssueCategoryRepository(_context);
        private IRoleRepository? _roleRepository;
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_context);
        private IIssuePriorityRepository? _issuePriorityRepository;
        public IIssuePriorityRepository IssuePriorityRepository => _issuePriorityRepository ??= new IssuePriorityRepository(_context);
        private IIssueImageRepository? _issueImageRepository;
        public IIssueImageRepository IssueImageRepository => _issueImageRepository ??= new IssueImageRepository(_context);
        private IIssueRepository? _issueRepository;
        public IIssueRepository IssueRepository => _issueRepository ??= new IssueRepository(_context);
        private IIssueMessageRepository? _issueMessageRepository;
        public IIssueMessageRepository IssueMessageRepository => _issueMessageRepository ??= new IssueMessageRepository(_context);
        private IIssueRemarkRepository? _issueRemarkRepository;
        public IIssueRemarkRepository IssueRemarkRepository => _issueRemarkRepository ??= new IssueRemarkRepository(_context);
        private IUserRepository? _userRepository;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

    }
}