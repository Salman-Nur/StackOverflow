
using Microsoft.EntityFrameworkCore;
using StackOverflow.Domain.Entities;
using System.Linq.Expressions;

namespace StackOverflow.Infrastructure.Repositories
{
    public class VoteRepository : Repository<Vote, Guid>, IVoteRepository
    {
        public VoteRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<IList<Vote>> GetByQuestionIdAndUserIdAsync(Guid questionId, Guid userId)
        {
            Expression<Func<Vote, bool>> filter = a => a.QuestionId == questionId && a.UserId == userId;
            var votes = await GetAsync(filter, null);
            return votes;
        }
        public async Task<IList<Vote>> GetByQuestionIdAsync(Guid questionId)
        {
            Expression<Func<Vote, bool>> filter = a => a.QuestionId == questionId;
            var votes = await GetAsync(filter, null);
            return votes;
        }
    }
}
