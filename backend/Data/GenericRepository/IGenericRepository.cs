namespace Data.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task SaveAsync();
       Task UpdateAsync(T entity);

        void Delete(T entity);
        
    }
}