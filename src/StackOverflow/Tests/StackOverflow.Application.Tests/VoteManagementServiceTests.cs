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

namespace StackOverflow.Application.Tests
{
    public class VoteManagementServiceTests
    {
        private AutoMock _mock;
        private Mock<IVoteRepository> _voteRepositoryMock;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private VoteManagementService _voteManagementService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _voteRepositoryMock = _mock.Mock<IVoteRepository>();
            _unitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _voteManagementService = _mock.Create<VoteManagementService>();
        }

        [TearDown]
        public void TearDown()
        {
            _voteRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateVoteAsync_WhenCalled_CreatesNewVote()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            int voteType = 1;

            var existingVotes = new List<Vote>();

            _unitOfWorkMock.Setup(x => x.VoteRepository.GetByQuestionIdAndUserIdAsync(questionId, userId))
                           .ReturnsAsync(existingVotes);

            _unitOfWorkMock.Setup(x => x.VoteRepository.AddAsync(It.IsAny<Vote>()))
                           .Returns(Task.CompletedTask)
                           .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync())
                           .Returns(Task.CompletedTask)
                           .Verifiable();

            // Act
            await _voteManagementService.CreateVoteAsync(questionId, userId, voteType);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => _unitOfWorkMock.VerifyAll(),
                () => _voteRepositoryMock.VerifyAll()
            );
        }


        [Test]
        public async Task GetVotesByQuestionIdAsync_WhenValidQuestionIdProvided_ReturnsVotes()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            var expectedVotes = new List<Vote>
            {
                new Vote { Id = Guid.NewGuid(), QuestionId = questionId, UserId = Guid.NewGuid(), VoteType = 1 },
                new Vote { Id = Guid.NewGuid(), QuestionId = questionId, UserId = Guid.NewGuid(), VoteType = -1 }
            };

            _unitOfWorkMock.Setup(x => x.VoteRepository.GetByQuestionIdAsync(questionId))
                .ReturnsAsync(expectedVotes);

            // Act
            var result = await _voteManagementService.GetVotesByQuestionIdAsync(questionId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedVotes);
        }
    }
}
