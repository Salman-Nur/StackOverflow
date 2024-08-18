
using StackOverflow.Domain.Entities;

namespace StackOverflow.Application.Features.Training.Services
{
	public interface IAnswerManagementService
	{
        Task CreateAnswerAsync(string answerText, Guid questionId, Guid userId, string userName);

        Task<IList<Answer>> GetAnswersByQuestionIdAsync(Guid questionId);
	}
}