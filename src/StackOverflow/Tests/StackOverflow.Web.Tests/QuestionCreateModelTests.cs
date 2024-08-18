using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using Shouldly;
using StackOverflow.Application;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.Repositories;
using StackOverflow.Web.Models;
using System;
using System.Threading.Tasks;

namespace StackOverflow.Web.Tests.Models
{
    public class QuestionCreateModelTests
    {
        private AutoMock _mock;
        private Mock<IQuestionManagementService> _questionManagementServiceMock;
        private QuestionCreateModel _questionCreateModel;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _questionManagementServiceMock = _mock.Mock<IQuestionManagementService>();
            _questionCreateModel = _mock.Create<QuestionCreateModel>();
        }

        [TearDown]
        public void TearDown()
        {
            _questionManagementServiceMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateQuestionAsync_WhenCalled_CallsQuestionManagementServiceWithCorrectParameters()
        {
            // Arrange
            Guid id = new Guid("06F10963-AE48-4E4D-A98A-4F2DFBF80E2A");
            Guid userId = Guid.NewGuid();
            const string title = "Sample Title";
            const string body = "Sample Body";
            const string tag = "SampleTag";

            _questionCreateModel.Id = id;
            _questionCreateModel.UserId = userId;
            _questionCreateModel.Title = title;
            _questionCreateModel.Body = body;
            _questionCreateModel.Tag = tag;

            // Act
            await _questionCreateModel.CreateQuestionAsync();

            // Assert
            _questionManagementServiceMock.Verify(x =>
                x.CreateQuestionAsync(id, title, body, tag, userId), Times.Once);
        }

        [Test]
        public async Task CreateTagAsync_WhenCalled_CallsQuestionManagementServiceCreateTagAsyncWithCorrectParameters()
        {
            // Arrange
            Guid questionId = new Guid("06F10963-AE48-4E4D-A98A-4F2DFBF80E2A");
            string tag = "SampleTag";

            _questionCreateModel.QuestionId = questionId;
            _questionCreateModel.Tag = tag;

            // Act
            await _questionCreateModel.CreateTagAsync();

            // Assert
            _questionManagementServiceMock.Verify(x =>
                x.CreateTagAsync(tag, questionId), Times.Once);
        }
    }
}
