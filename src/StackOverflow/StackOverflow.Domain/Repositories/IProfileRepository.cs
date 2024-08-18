using StackOverflow.Domain.Entities;
using StackOverflow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface IProfileRepository : IRepositoryBase<User,Guid>
    {
        Task<bool> IsUserNameDuplicateAsync(string userName, Guid? id = null);
        Task<User> GetByUserNameAsync(string userName);
    }
}
