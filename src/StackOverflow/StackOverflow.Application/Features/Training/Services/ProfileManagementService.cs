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
	public class ProfileManagementService : IProfileManagementService
	{
		private readonly IApplicationUnitOfWork _unitOfWork;
        public ProfileManagementService(IApplicationUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public async Task EditProfileAsync(Guid id, string userName, string title, string description, string country, string imageUrl)
        {
            User user = await _unitOfWork.ProfileRepository.GetByIdAsync(id);

            if (user == null)
            {
                user = new User { Id = id };
                if (userName != null)
                {
                    user.UserName = userName;
                }

                if (title != null)
                {
                    user.Title = title;
                }

                if (description != null)
                {
                    user.Description = description;
                }

                if (country != null)
                {
                    user.Country = country;
                }

                if (imageUrl != null)
                {
                    user.ImageUrl = imageUrl;
                }

                await _unitOfWork.ProfileRepository.AddAsync(user);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                if (userName != null)
                {
                    if (user.UserName != userName)
                    {
                        bool isDuplicateUserName = await _unitOfWork.ProfileRepository.
                            IsUserNameDuplicateAsync(userName);

                        if (isDuplicateUserName)
                            throw new DuplicateUserNameException();
                    }

                    user.UserName = userName;
                }

                if (title != null)
                {
                    user.Title = title;
                }

                if (description != null)
                {
                    user.Description = description;
                }

                if (country != null)
                {
                    user.Country = country;
                }
                if (imageUrl != null)
                {
                    user.ImageUrl = imageUrl;
                }

                await _unitOfWork.ProfileRepository.EditAsync(user);
                await _unitOfWork.SaveAsync();
            }           
        }

        public async Task<User> GetProfileAsync(Guid id)
		{
            var user = await _unitOfWork.ProfileRepository.GetByIdAsync(id);
            if (user is not null)
            {
                user.Questions = await _unitOfWork.QuestionRepository.GetQuestionCountByUserIdAsync(id);
                user.Answers = await _unitOfWork.AnswerRepository.GetAnswerCountByUserIdAsync(id);
            }
            return user;
        }

        public async Task<User> GetProfileByUserNameAsync(string userName)
        {
            var user = await _unitOfWork.ProfileRepository.GetByUserNameAsync(userName);
            return user;
        }
    }
}
