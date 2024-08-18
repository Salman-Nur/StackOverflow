
using Microsoft.EntityFrameworkCore;
using StackOverflow.Domain.Entities;
using System.Linq.Expressions;

namespace StackOverflow.Infrastructure.Repositories
{
    public class TagRepository : Repository<Tag, Guid>, ITagRepository
    {
        public TagRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

		public async Task<(IList<Tag> records, int total, int totalDisplay)> GetTableDataAsync(string searchText, string orderBy, int pageIndex, int pageSize)
		{
			Expression<Func<Tag, bool>> expression = null;
			if (!string.IsNullOrWhiteSpace(searchText))
			{
				expression = x => x.TagName.Contains(searchText);
			}
			return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
		}
	}
}
