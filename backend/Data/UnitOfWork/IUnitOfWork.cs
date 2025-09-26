using Data.Repositories.Interfaces;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();

        IActionTypeRepository ActionTypeRepository { get; }
        IAuditTrailRepository AuditTrailRepository { get; }
        IImageRepository ImageRepository { get; }
        IIssueRepository IssueRepository { get; }
        IMessageRepository MessageRepository { get; }
        IRemarkRepository RemarkRepository { get; }
        IUserRepository UserRepository { get; }
       
    }
}