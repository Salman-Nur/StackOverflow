using Autofac.Extras.Moq;
using Moq;
using Shouldly;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Azure;

namespace StackOverflow.Application.Tests
{
    public class CommentManagementServiceTests
    {
        private AutoMock _mock;
        private Mock<ICommentRepository> _commentRepositoryMock;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private CommentManagementService _commentManagementService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _commentRepositoryMock = _mock.Mock<ICommentRepository>();
            _unitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _commentManagementService = _mock.Create<CommentManagementService>();
        }

        [TearDown]
        public void TearDown()
        {
            _commentRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateCommentAsync_WhenCalled_CreatesNewComment()
        {
            // Arrange
            const string commentText = "Sample comment text";
            Guid userId = Guid.NewGuid();
            Guid questionId = Guid.NewGuid();
            const string userName = "salman";

            var comment = new Comment
            {
                CommentText = commentText,
                QuestionId = questionId,
                UserId = userId,
                UserName = userName
            };

            _unitOfWorkMock.SetupGet(x => x.CommentRepository).Returns(_commentRepositoryMock.Object).Verifiable();

            _commentRepositoryMock.Setup(x => x.AddAsync(It.Is<Comment>(y => 
            y.CommentText == commentText && 
            y.QuestionId == questionId && 
            y.UserId == userId && 
            y.UserName == userName))).Returns(Task.CompletedTask).Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _commentManagementService.CreateCommentAsync(commentText, userId, questionId, userName);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => _unitOfWorkMock.VerifyAll(),
                () => _commentRepositoryMock.VerifyAll()
            );
        }

        [Test]
        public async Task GetCommentsByQuestionIdAsync_WhenValidQuestionIdProvided_ReturnsComments()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            var expectedComments = new List<Comment>
            {
                new Comment { Id = Guid.NewGuid(), CommentText = "Comment 1", QuestionId = questionId },
                new Comment { Id = Guid.NewGuid(), CommentText = "Comment 2", QuestionId = questionId }
            };
            _unitOfWorkMock.Setup(x => x.CommentRepository.GetByQuestionIdAsync(questionId)).ReturnsAsync(expectedComments);

            // Act
            var result = await _commentManagementService.GetCommentsByQuestionIdAsync(questionId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedComments);
        }
    }
}
