
using StackOverflow.Domain.Entities;

namespace StackOverflow.Application.Features.Training.Services
{
	public interface IVoteManagementService
	{
		Task CreateVoteAsync(Guid questionId, Guid userId, int voteType);
        Task<IList<Vote>> GetVotesByQuestionIdAsync(Guid questionId);
    }
}