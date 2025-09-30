
using Data.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Model;

using Data.GenericRepository;

namespace Data.Repositories.Interfaces
{
    public interface IIssueRepository : IGenericRepository<Issue>
    {
        Task<int> CountAsync();
        Task<int> CountByConditionAsync(Expression<Func<Issue, bool>> predicate);
    }
}