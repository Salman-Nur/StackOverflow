using StackOverflow.Domain.Entities;
using StackOverflow.Domain.Exceptions;
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
	public class QuestionManagementService : IQuestionManagementService
	{
		private readonly IApplicationUnitOfWork _unitOfWork;
        public QuestionManagementService(IApplicationUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task CreateQuestionAsync(Guid id, string title, string body, string Tag, Guid userId)
		{
            bool isDuplicateTitle = await _unitOfWork.QuestionRepository.
                IsTitleDuplicateAsync(title);

            if (isDuplicateTitle)
                throw new DuplicateTitleException();

            Question question = new Question
			{
				Id = id,
				Title = title,
				Body = body,
				Tag = Tag,
				UserId = userId
			};

			await _unitOfWork.QuestionRepository.AddAsync(question);
			await _unitOfWork.SaveAsync();
        }

        public async Task CreateTagAsync(string tagName, Guid questionId)
        {
            Tag tag = new Tag
            {
                TagName = tagName,
                QuestionId = questionId
            };

            await _unitOfWork.TagRepository.AddAsync(tag);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Question> records, int total, int totalDisplay)> 
            GetPagedMyQuestionsAsync(Guid userId, int pageIndex, int pageSize, string searchText, string sortBy)
        {
            return await _unitOfWork.QuestionRepository.GetMyTableDataAsync(userId, searchText, sortBy, pageIndex, pageSize);
        }

        public async Task<(IList<Question> records, int total, int totalDisplay)> 
			GetPagedQuestionsAsync(int pageIndex, int pageSize, string searchText, string sortBy)
        {
            return await _unitOfWork.QuestionRepository.GetTableDataAsync(searchText, sortBy, pageIndex, pageSize);
        }

        public async Task<Question> GetQuestionAsync(Guid id)
		{
			return await _unitOfWork.QuestionRepository.GetByIdAsync(id);
		}
    }
}
