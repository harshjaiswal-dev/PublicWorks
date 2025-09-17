using Data;
using Data.GenericRepository;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Implementations.Repositories
{
    public class RemarkRepository : GenericRepository<Remark>, IRemarkRepository
    {
        private readonly AppDbContext _context;

        public RemarkRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}