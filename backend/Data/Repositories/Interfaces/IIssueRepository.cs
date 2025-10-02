using Data.Model;
using Data.GenericRepository;
using System.Linq.Expressions;

namespace Data.Repositories.Interfaces
{
    public interface IIssueRepository : IGenericRepository<Issue>
    {
        Task<int> CountAsync();
        Task<int> CountByConditionAsync(Expression<Func<Issue, bool>> predicate);
    }
}