using Data.Repositories.Interfaces;

namespace Data.UnitOfWork
{
    public interface IUoW : IDisposable
    {
        Task<int> CompleteAsync();

        IUserRepository UserRepository { get; }
       
    }
}