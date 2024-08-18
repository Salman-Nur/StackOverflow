using StackOverflow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Features.Training.Services
{
	public class AnswerManagementService : IAnswerManagementService
	{
		private readonly IApplicationUnitOfWork _unitOfWork;
        public AnswerManagementService(IApplicationUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public async Task CreateAnswerAsync(string answerText, Guid questionId, Guid userId, string userName)
        {
			Answer answer = new Answer()
			{
				AnswerText = answerText,
				QuestionId = questionId,
                UserId = userId,
                UserName = userName
			};
			await _unitOfWork.AnswerRepository.AddAsync(answer);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IList<Answer>> GetAnswersByQuestionIdAsync(Guid questionId)
        {
            return await _unitOfWork.AnswerRepository.GetByQuestionIdAsync(questionId);
        }
	}
}
