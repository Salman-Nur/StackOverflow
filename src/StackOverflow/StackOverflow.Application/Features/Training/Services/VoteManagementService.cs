using StackOverflow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Features.Training.Services
{
    public class VoteManagementService : IVoteManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public VoteManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateVoteAsync(Guid questionId, Guid userId, int voteType)
        {
            var existingVote = await _unitOfWork.VoteRepository.
                GetByQuestionIdAndUserIdAsync(questionId, userId);

            if (existingVote.Count() % 2 == 0 && existingVote.Count > 0)
            {
                foreach (var item in existingVote)
                {
                    _unitOfWork.VoteRepository.Remove(item.Id);
                }

                await _unitOfWork.SaveAsync();
            }
            if (existingVote.Count() % 2 == 1)
            {
                foreach (var item in existingVote)
                {
                    if (item.VoteType == voteType)
                    {
                        return;
                    }
                }
            }

            Vote vote = new Vote
            {
                QuestionId = questionId,
                UserId = userId,
                VoteType = voteType
            };

            await _unitOfWork.VoteRepository.AddAsync(vote);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IList<Vote>> GetVotesByQuestionIdAsync(Guid questionId)
        {
            return await _unitOfWork.VoteRepository.GetByQuestionIdAsync(questionId);
        }
    }
}
