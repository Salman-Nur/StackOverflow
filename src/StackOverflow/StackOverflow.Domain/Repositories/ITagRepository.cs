using StackOverflow.Domain.Entities;
using StackOverflow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface ITagRepository : IRepositoryBase<Tag,Guid>
    {
		Task<(IList<Tag> records, int total, int totalDisplay)>
			GetTableDataAsync(string searchText, string orderBy, int pageIndex, int pageSize);
	}
}
