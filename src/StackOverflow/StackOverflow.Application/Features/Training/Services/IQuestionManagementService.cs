
using StackOverflow.Domain.Entities;

namespace StackOverflow.Application.Features.Training.Services
{
	public interface IQuestionManagementService
	{
		Task CreateQuestionAsync(Guid id, string title, string body, string tag,  Guid userId);
        Task<Question> GetQuestionAsync(Guid id);
        Task CreateTagAsync(string tagName, Guid questionId);
        Task<(IList<Question> records, int total, int totalDisplay)>
           GetPagedQuestionsAsync(int pageIndex, int pageSize, string searchText, string sortBy);

        Task<(IList<Question> records, int total, int totalDisplay)>
           GetPagedMyQuestionsAsync(Guid userId, int pageIndex, int pageSize, string searchText, string sortBy);
    }
}