using Data.GenericRepository;
using Data.Model;

namespace Business.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Message> Message { get; }
        IGenericRepository<Status> Status { get; }
        

        Task<int> CompleteAsync(); // Save changes
    }
}
