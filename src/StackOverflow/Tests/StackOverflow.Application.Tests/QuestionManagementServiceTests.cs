using Autofac.Extras.Moq;
using Azure;
using Moq;
using Shouldly;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Domain.Exceptions;
using StackOverflow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Tests
{
    public class QuestionManagementServiceTests
    {
        private AutoMock _mock;
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private QuestionManagementService _questionManagementService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _questionRepositoryMock = _mock.Mock<IQuestionRepository>();
            _unitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _questionManagementService = _mock.Create<QuestionManagementService>();
        }

        [TearDown]
        public void TearDown()
        {
            _questionRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateQuestionAsync_TitleUnique_CreatesNewQuestion()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            const string title = "title";
            const string body = "body";
            const string tag = "tag";
            Guid userId = Guid.NewGuid();

            var question = new Question
            {
                Id = id,
                Title = title,
                Body = body,
                Tag = tag,
                UserId = userId
            };

            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(x => x.IsTitleDuplicateAsync(title, null)).ReturnsAsync(false).Verifiable();

            _questionRepositoryMock.Setup(x => x.AddAsync(It.Is<Question>(y => y.Title == title
                && y.Body == body && y.Tag == tag))).Returns(Task.CompletedTask).Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _questionManagementService.CreateQuestionAsync(id, title, body, tag, userId);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => _unitOfWorkMock.VerifyAll(),
                () => _questionRepositoryMock.VerifyAll()
            );
        }

        [Test]
        public async Task CreateQuestionAsync_TitleDuplicate_ThrowsException()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            const string title = "title";
            const string body = "body";
            const string tag = "tag";
            Guid userId = Guid.NewGuid();

            var question = new Question
            {
                Id = id,
                Title = title,
                Body = body,
                Tag = tag,
                UserId = userId
            };

            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(x => x.IsTitleDuplicateAsync(title, null)).ReturnsAsync(true).Verifiable();

            // Act && Assert
            await Should.ThrowAsync<DuplicateTitleException>(async () =>
            await _questionManagementService.CreateQuestionAsync(id, title, body, tag, userId)
            );
        }

        [Test]
        public async Task GetQuestionAsync_WhenValidIdProvided_ReturnsQuestionSuccessfully()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            var expectedQuestion = new Question { Id = questionId };

            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object);
            _questionRepositoryMock.Setup(x => x.GetByIdAsync(questionId)).ReturnsAsync(expectedQuestion);

            // Act
            var result = await _questionManagementService.GetQuestionAsync(questionId);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(questionId);

        }

        [Test]
        public async Task GetPagedQuestionsAsync_WhenValidParametersProvided_ReturnsPagedQuestions()
        {
            // Arrange
            const int pageIndex = 1;
            const int pageSize = 10;
            const string searchText = "title";
            const string sortBy = "createdAt";

            var expectedQuestions = new List<Question>
            {
                new Question { Id = Guid.NewGuid(), Title = "Title 1", Body = "Body 1" },
                new Question { Id = Guid.NewGuid(), Title = "Title 2", Body = "Body 2" }
            };
            var expectedTotal = expectedQuestions.Count;
            var expectedTotalDisplay = expectedQuestions.Count;

            _unitOfWorkMock.Setup(x => x.QuestionRepository.GetTableDataAsync(searchText, sortBy, pageIndex, pageSize))
                .ReturnsAsync((expectedQuestions, expectedTotal, expectedTotalDisplay));

            // Act
            var result = await _questionManagementService.GetPagedQuestionsAsync(pageIndex, pageSize, searchText, sortBy);

            // Assert
            result.records.ShouldBe(expectedQuestions);
            result.total.ShouldBe(expectedTotal);
            result.totalDisplay.ShouldBe(expectedTotalDisplay);
        }
    }
}
