using StackOverflow.Domain.Entities;
using StackOverflow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface IQuestionRepository : IRepositoryBase<Question,Guid>
    {
        Task<bool> IsTitleDuplicateAsync(string title, Guid? id = null);
        Task<(IList<Question> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy, int pageIndex, int pageSize);

        Task<(IList<Question> records, int total, int totalDisplay)>
            GetMyTableDataAsync(Guid userId, string searchText, string orderBy, int pageIndex, int pageSize);

        Task<int> GetQuestionCountByUserIdAsync(Guid userId);
    }
}
