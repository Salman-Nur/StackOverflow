using StackOverflow.Domain.Entities;
using StackOverflow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface IAnswerRepository : IRepositoryBase<Answer,Guid>
    {
        Task<IList<Answer>> GetByQuestionIdAsync(Guid questionId);
        Task<int> GetAnswerCountByUserIdAsync(Guid userId);
    }
}
