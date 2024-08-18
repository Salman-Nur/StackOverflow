using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Tests.Models
{
    public class VoteModelTests
    {
        private AutoMock _mock;
        private Mock<IVoteManagementService> _voteManagementServiceMock;
        private VoteModel _voteModel;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _voteManagementServiceMock = _mock.Mock<IVoteManagementService>();
            _voteModel = _mock.Create<VoteModel>();
        }

        [TearDown]
        public void TearDown()
        {
            _voteManagementServiceMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateVoteAsync_WhenCalled_CallsVoteManagementServiceWithCorrectParameters()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var voteType = 1;

            _voteModel.QuestionId = questionId;
            _voteModel.UserId = userId;
            _voteModel.VoteType = voteType;

            // Act
            await _voteModel.CreateVoteAsync();

            // Assert
            _voteManagementServiceMock.Verify(x =>
                x.CreateVoteAsync(questionId, userId, voteType), Times.Once);
        }
    }
}
