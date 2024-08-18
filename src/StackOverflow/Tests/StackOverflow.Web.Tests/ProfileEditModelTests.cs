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
    public class ProfileEditModelTests
    {
        private AutoMock _mock;
        private Mock<IProfileManagementService> _profileManagementServiceMock;
        private Mock<IMapper> _mapperMock;
        private ProfileEditModel _profileEditModel;

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
            _profileEditModel = _mock.Create<ProfileEditModel>();
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
        public async Task EditProfileAsync_WhenCalled_CallsEditProfileAsyncOfProfileManagementServiceWithCorrectParameters()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string userName = "TestUser";
            string title = "Title";
            string description = "Description";
            string country = "Country";
            string imageUrl = "ImageUrl";

            _profileEditModel.UserId = userId;
            _profileEditModel.UserName = userName;
            _profileEditModel.Title = title;
            _profileEditModel.Description = description;
            _profileEditModel.Country = country;
            _profileEditModel.ImageUrl = imageUrl;

            // Act
            await _profileEditModel.EditProfileAsync();

            // Assert
            _profileManagementServiceMock.Verify(x =>
                x.EditProfileAsync(userId, userName, title, description, country, imageUrl), Times.Once);
        }

        [Test]
        public async Task LoadAsync_WhenCalled_LoadsProfileAndMapsToModel()
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
            await _profileEditModel.LoadAsync(userId);

            // Assert
            _mapperMock.Verify(x => x.Map(user, _profileEditModel), Times.Once);
        }
    }
}
