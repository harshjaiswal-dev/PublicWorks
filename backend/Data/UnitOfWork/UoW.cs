using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;

namespace Data.UnitOfWork
{
    public class UoW : IUoW
    {
        private readonly AppDbContext _context;
        public UoW(AppDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }


        private IUserRepository? _userRepository;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
     
    }
}