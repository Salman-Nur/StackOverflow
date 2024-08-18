
using StackOverflow.Domain.Entities;

namespace StackOverflow.Application.Features.Training.Services
{
	public interface ITagManagementService
	{
        Task<(IList<Tag> records, int total, int totalDisplay)>
           GetPagedTagsAsync(int pageIndex, int pageSize, string searchText, string sortBy);
	}
}