
using StackOverflow.Domain.Entities;

namespace StackOverflow.Application.Features.Training.Services
{
	public interface IProfileManagementService
	{
		Task EditProfileAsync(Guid id, string userName, string title, string description, 
            string country, string imageUrl);
        Task<User> GetProfileAsync(Guid id);
        Task<User> GetProfileByUserNameAsync(string userName);
    }
}