using Autofac;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Web.Models;
using System;
using System.Threading.Tasks;

namespace StackOverflow.Web.Tests.Models
{
    public class CommentCreateModelTests
    {
        private AutoMock _mock;
        private Mock<ICommentManagementService> _commentManagementServiceMock;
        private CommentCreateModel _commentCreateModel;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _commentManagementServiceMock = _mock.Mock<ICommentManagementService>();
            _commentCreateModel = _mock.Create<CommentCreateModel>();
        }

        [TearDown]
        public void TearDown()
        {
            _commentManagementServiceMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateCommentAsync_WhenCalled_CallsCommentManagementServiceWithCorrectParameters()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid questionId = Guid.NewGuid();
            const string userName = "SampleUserName";
            const string commentText = "Sample Comment";

            _commentCreateModel.UserId = userId;
            _commentCreateModel.QuestionId = questionId;
            _commentCreateModel.UserName = userName;
            _commentCreateModel.CommentText = commentText;

            // Act
            await _commentCreateModel.CreateCommentAsync();

            // Assert
            _commentManagementServiceMock.Verify(x =>
                x.CreateCommentAsync(commentText, userId, questionId, userName), Times.Once);
        }
    }
}
