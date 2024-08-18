using Autofac.Extras.Moq;
using Azure;
using Moq;
using Shouldly;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Tests
{
    public class AnswerManagementServiceTests
    {
        private AutoMock _mock;
        private Mock<IAnswerRepository> _answerRepositoryMock;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private AnswerManagementService _answerManagementService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _answerRepositoryMock = _mock.Mock<IAnswerRepository>();
            _unitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _answerManagementService = _mock.Create<AnswerManagementService>();
        }

        [TearDown]
        public void TearDown()
        {
            _answerRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateAnswerAsync_WhenCalled_CreatesNewAnswer()
        {
            // Arrange
            const string answerText = "Sample answer text";
            Guid questionId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            const string userName = "salman";

            var answer = new Answer
            {
                AnswerText = answerText,
                QuestionId = questionId,
                UserId = userId,
                UserName = userName
            };

            _unitOfWorkMock.SetupGet(x => x.AnswerRepository).Returns(_answerRepositoryMock.Object).Verifiable();

            _answerRepositoryMock.Setup(x => x.AddAsync(It.Is<Answer>(y => 
            y.AnswerText == answerText && 
            y.QuestionId == questionId &&
            y.UserId == userId &&
            y.UserName == userName))).Returns(Task.CompletedTask).Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _answerManagementService.CreateAnswerAsync(answerText, questionId, userId, userName);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => _unitOfWorkMock.VerifyAll(),
                () => _answerRepositoryMock.VerifyAll()
            );
        }

        [Test]
        public async Task GetAnswersByQuestionIdAsync_WhenValidQuestionIdProvided_ReturnsAnswers()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            var expectedAnswers = new List<Answer>
            {
                new Answer { Id = Guid.NewGuid(), AnswerText = "Answer 1", QuestionId = questionId },
                new Answer { Id = Guid.NewGuid(), AnswerText = "Answer 2", QuestionId = questionId }
            };
            _unitOfWorkMock.Setup(x => x.AnswerRepository.GetByQuestionIdAsync(questionId)).ReturnsAsync(expectedAnswers);

            // Act
            var result = await _answerManagementService.GetAnswersByQuestionIdAsync(questionId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedAnswers);
        }
    }
}
