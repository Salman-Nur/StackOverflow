using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Autofac;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Tests.Models
{
    public class QuestionDetailsModelTests
    {
        private AutoMock _mock;
        private Mock<IQuestionManagementService> _questionManagementServiceMock;
        private Mock<IAnswerManagementService> _answerManagementServiceMock;
        private Mock<ICommentManagementService> _commentManagementServiceMock;
        private Mock<IVoteManagementService> _voteManagementServiceMock;
        private Mock<IMapper> _mapperMock;
        private QuestionDetailsModel _questionDetailsModel;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _questionManagementServiceMock = _mock.Mock<IQuestionManagementService>();
            _answerManagementServiceMock = _mock.Mock<IAnswerManagementService>();
            _commentManagementServiceMock = _mock.Mock<ICommentManagementService>();
            _voteManagementServiceMock = _mock.Mock<IVoteManagementService>();
            _mapperMock = _mock.Mock<IMapper>();
            _questionDetailsModel = _mock.Create<QuestionDetailsModel>();
        }

        [TearDown]
        public void TearDown()
        {
            _questionManagementServiceMock?.Reset();
            _answerManagementServiceMock?.Reset();
            _commentManagementServiceMock?.Reset();
            _voteManagementServiceMock?.Reset();
            _mapperMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task LoadAsync_WhenCalled_LoadsQuestionAndMapsToModel()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            var question = new Question
            {
                Id = questionId,
                UserId = Guid.NewGuid(),
                Title = "Title",
                Body = "Body",
            };

            _questionManagementServiceMock.Setup(x => x.GetQuestionAsync(questionId)).ReturnsAsync(question);

            // Act
            await _questionDetailsModel.LoadAsync(questionId);

            // Assert
            _mapperMock.Verify(x => x.Map(question, _questionDetailsModel), Times.Once);
        }

        [Test]
        public async Task LoadAnswerAsync_WhenCalled_LoadsAnswersForQuestion()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            var answers = new List<Answer>
            {
                new Answer { AnswerText = "Answer1" },
                new Answer { AnswerText = "Answer2" }
            };

            _answerManagementServiceMock.Setup(x => x.GetAnswersByQuestionIdAsync(questionId)).ReturnsAsync(answers);

            // Act
            await _questionDetailsModel.LoadAnswerAsync(questionId);

            // Assert
            Assert.That(_questionDetailsModel.Answers, Is.EqualTo(answers));
        }

        [Test]
        public async Task LoadCommentAsync_WhenCalled_LoadsCommentsForQuestion()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            var comments = new List<Comment>
            {
                new Comment { CommentText = "Comment1" },
                new Comment { CommentText = "Comment2" }
            };

            _commentManagementServiceMock.Setup(x => x.GetCommentsByQuestionIdAsync(questionId)).ReturnsAsync(comments);

            // Act
            await _questionDetailsModel.LoadCommentAsync(questionId);

            // Assert
            Assert.That(_questionDetailsModel.Comments, Is.EqualTo(comments));
        }

        [Test]
        public async Task LoadVoteAsync_WhenCalled_LoadsVotesForQuestion()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();
            var votes = new List<Vote>
            {
                new Vote { VoteType = 1 },
                new Vote { VoteType = -1 }
            };

            _voteManagementServiceMock.Setup(x => x.GetVotesByQuestionIdAsync(questionId)).ReturnsAsync(votes);

            // Act
            await _questionDetailsModel.LoadVoteAsync(questionId);

            // Assert
            Assert.That(_questionDetailsModel.TotalVote, Is.EqualTo(0));
        }
    }
}
