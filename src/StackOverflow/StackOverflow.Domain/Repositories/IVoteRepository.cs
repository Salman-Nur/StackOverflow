using StackOverflow.Domain.Entities;
using StackOverflow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface IVoteRepository : IRepositoryBase<Vote,Guid>
    {
        Task<IList<Vote>> GetByQuestionIdAsync(Guid questionId);
        Task<IList<Vote>> GetByQuestionIdAndUserIdAsync(Guid questionId, Guid userId);
    }
}
