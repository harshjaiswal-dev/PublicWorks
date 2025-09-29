using Data.Repositories.Interfaces;

namespace Data.UnitOfWork
{
    /// <summary>
    /// Defines a unit of work for managing multiple repositories
    /// and committing changes to the database in a single transaction.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits all changes made in the unit of work asynchronously.
        /// </summary>
        /// <returns>Number of state entries written to the database.</returns>
        Task<int> SaveAsync();

        IActionTypeRepository ActionTypeRepository { get; }
        IIssueRepository IssueRepository { get; }
        IIssueCategoryRepository IssueCategoryRepository { get; }
        IIssueImageRepository IssueImageRepository { get; }
        IIssueMessageRepository IssueMessageRepository { get; }
        IIssuePriorityRepository IssuePriorityRepository { get; }
        IIssueRemarkRepository IssueRemarkRepository { get; }
        IIssueStatusRepository IssueStatusRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }

    }
}