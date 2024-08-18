
using Microsoft.EntityFrameworkCore;
using StackOverflow.Domain.Entities;
using System.Linq.Expressions;

namespace StackOverflow.Infrastructure.Repositories
{
    public class QuestionRepository : Repository<Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<bool> IsTitleDuplicateAsync(string title, Guid? id = null)
        {
            if (id.HasValue)
            {
                return (await GetCountAsync(x => x.Id != id.Value && x.Title == title)) > 0;
            }
            else
            {
                return (await GetCountAsync(x => x.Title == title)) > 0;
            }
        }

        public async Task<(IList<Question> records, int total, int totalDisplay)> 
            GetMyTableDataAsync(Guid userId, string searchText, string orderBy, int pageIndex, int pageSize)
        {
            Expression<Func<Question, bool>> expression = null;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                expression = x => x.UserId == userId && x.Title.Contains(searchText);
            }
            else
            {
                expression = x => x.UserId == userId;
            }
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }

        public async Task<(IList<Question> records, int total, int totalDisplay)> GetTableDataAsync(string searchText, string orderBy, int pageIndex, int pageSize)
        {
            Expression<Func<Question, bool>> expression = null;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                expression = x => x.Title.Contains(searchText);
            }
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }

        public async Task<int> GetQuestionCountByUserIdAsync(Guid userId)
        {
            Expression<Func<Question, bool>> filter = q => q.UserId == userId;

            int count = await GetCountAsync(filter);

            return count;
        }
    }
}
