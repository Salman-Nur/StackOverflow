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
	public class CommentManagementService : ICommentManagementService
	{
		private readonly IApplicationUnitOfWork _unitOfWork;
        public CommentManagementService(IApplicationUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public async Task CreateCommentAsync(string commentText, Guid userId, Guid questionId, string userName)
        {
			Comment comment = new Comment()
			{
				CommentText = commentText,
                UserId = userId,
                QuestionId = questionId,
                UserName = userName
			};
			await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IList<Comment>> GetCommentsByQuestionIdAsync(Guid questionId)
        {
            return await _unitOfWork.CommentRepository.GetByQuestionIdAsync(questionId);
        }
	}
}
