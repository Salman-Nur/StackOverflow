using Autofac.Extras.Moq;
using Moq;
using Shouldly;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using StackOverflow.Domain.Exceptions;
using Azure;

namespace StackOverflow.Application.Tests
{
    public class ProfileManagementServiceTests
    {
        private AutoMock _mock;
        private Mock<IProfileRepository> _profileRepositoryMock;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private ProfileManagementService _profileManagementService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _profileRepositoryMock = _mock.Mock<IProfileRepository>();
            _unitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _profileManagementService = _mock.Create<ProfileManagementService>();
        }

        [TearDown]
        public void TearDown()
        {
            _profileRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task EditProfileAsync_WhenUserFound_EditsExistingProfile()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            const string userName = "salman";
            const string title = "student";
            const string description = "description";
            const string country = "bd";
            const string imageUrl = "Capture.PNG";

            var existingUser = new User { Id = userId, UserName = "OldUserName", Title = "OldTitle", Description = "OldDescription", Country = "OldCountry", ImageUrl = "OldImageUrl" };

            _unitOfWorkMock.Setup(x => x.ProfileRepository.GetByIdAsync(userId)).ReturnsAsync(existingUser);

            // Act
            await _profileManagementService.EditProfileAsync(userId, userName, title, description, country, imageUrl);

            // Assert
            existingUser.UserName.ShouldBe(userName);
            existingUser.Title.ShouldBe(title);
            existingUser.Description.ShouldBe(description);
            existingUser.Country.ShouldBe(country);
            existingUser.ImageUrl.ShouldBe(imageUrl);
        }


        [Test]
        public async Task GetProfileAsync_WhenValidIdProvided_ReturnsUser()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var expectedProfile = new User { Id = userId, UserName = "salman", Title = "student", Description = "description", Country = "bd", ImageUrl = "Capture.PNG" };

            var questionCount = 5;
            var answerCount = 10;

            _unitOfWorkMock.Setup(x => x.ProfileRepository.GetByIdAsync(userId)).ReturnsAsync(expectedProfile);
            _unitOfWorkMock.Setup(x => x.QuestionRepository.GetQuestionCountByUserIdAsync(userId)).ReturnsAsync(questionCount);
            _unitOfWorkMock.Setup(x => x.AnswerRepository.GetAnswerCountByUserIdAsync(userId)).ReturnsAsync(answerCount);

            // Act
            var result = await _profileManagementService.GetProfileAsync(userId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedProfile);
            result.Questions.ShouldBe(questionCount);
            result.Answers.ShouldBe(answerCount);
        }

        [Test]
        public async Task GetProfileByUserNameAsync_ExistingUserName_ReturnsUser()
        {
            // Arrange
            string userName = "salman";
            var expectedProfile = new User { Id = Guid.NewGuid(), UserName = userName, Title = "student", Description = "description", Country = "bd", ImageUrl = "Capture.PNG" };

            _unitOfWorkMock.Setup(x => x.ProfileRepository.GetByUserNameAsync(userName)).ReturnsAsync(expectedProfile);

            // Act
            var result = await _profileManagementService.GetProfileByUserNameAsync(userName);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedProfile);
        }
    }
}
