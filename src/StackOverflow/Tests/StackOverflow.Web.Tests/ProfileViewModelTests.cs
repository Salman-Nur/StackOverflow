using System;
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
    public class ProfileViewModelTests
    {
        private AutoMock _mock;
        private Mock<IProfileManagementService> _profileManagementServiceMock;
        private Mock<IMapper> _mapperMock;
        private ProfileViewModel _profileViewModel;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _profileManagementServiceMock = _mock.Mock<IProfileManagementService>();
            _mapperMock = _mock.Mock<IMapper>();
            _profileViewModel = _mock.Create<ProfileViewModel>();
        }

        [TearDown]
        public void TearDown()
        {
            _profileManagementServiceMock?.Reset();
            _mapperMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task ViewProfileAsync_WhenCalled_LoadsProfileAndMapsToViewModel()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                UserName = "TestUser",
                Title = "Title",
                Description = "Description",
                Country = "Country",
                ImageUrl = "ImageUrl"
            };

            _profileManagementServiceMock.Setup(x => x.GetProfileAsync(userId)).ReturnsAsync(user);

            // Act
            await _profileViewModel.ViewProfileAsync(userId);

            // Assert
            _mapperMock.Verify(x => x.Map(user, _profileViewModel), Times.Once);
        }
    }
}
