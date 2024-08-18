using StackOverflow.Domain.Entities;
using StackOverflow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface ICommentRepository : IRepositoryBase<Comment,Guid>
    {
        Task<IList<Comment>> GetByQuestionIdAsync(Guid questionId);
    }
}
