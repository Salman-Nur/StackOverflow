
using Microsoft.EntityFrameworkCore;
using StackOverflow.Domain.Entities;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace StackOverflow.Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comment, Guid>, ICommentRepository
    {
        public CommentRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<IList<Comment>> GetByQuestionIdAsync(Guid questionId)
        {
            Expression<Func<Comment, bool>> filter = a => a.QuestionId == questionId;
            var comments = await GetAsync(filter, null);
            return comments;
        }
    }
}
