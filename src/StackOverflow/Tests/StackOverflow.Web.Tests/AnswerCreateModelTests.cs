using Autofac;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using Shouldly;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Web.Models;
using System;
using System.Threading.Tasks;

namespace StackOverflow.Web.Tests.Models
{
    public class AnswerCreateModelTests
    {
        private AutoMock _mock;
        private Mock<IAnswerManagementService> _answerManagementServiceMock;
        private AnswerCreateModel _answerCreateModel;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _answerManagementServiceMock = _mock.Mock<IAnswerManagementService>();
            _answerCreateModel = _mock.Create<AnswerCreateModel>();
        }

        [TearDown]
        public void TearDown()
        {
            _answerManagementServiceMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateAnswerAsync_WhenCalled_CallsAnswerManagementServiceWithCorrectParameters()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid questionId = Guid.NewGuid();
            const string userName = "salman";
            const string answerText = "Sample Answer";

            _answerCreateModel.UserId = userId;
            _answerCreateModel.QuestionId = questionId;
            _answerCreateModel.UserName = userName;
            _answerCreateModel.AnswerText = answerText;

            // Act
            await _answerCreateModel.CreateAnswerAsync();

            // Assert
            _answerManagementServiceMock.Verify(x =>
                x.CreateAnswerAsync(answerText, questionId, userId, userName), Times.Once);
        }
    }
}
