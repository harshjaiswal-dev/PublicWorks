using System.Threading.Tasks;
using Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Data.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(T).Name, id);

            return entity;
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new NullEntityException(typeof(T).Name);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            if (entity == null)
                throw new NullEntityException(typeof(T).Name);

            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
                throw new EntityNotFoundException(typeof(T).Name, id);

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
                throw new EntityNotFoundException(typeof(T).Name, id);

            _dbSet.Remove(existingEntity);

        }

    }
}