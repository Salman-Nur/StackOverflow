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
    public class TagManagementServiceTests
    {
        private AutoMock _mock;
        private Mock<ITagRepository> _tagRepositoryMock;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private TagManagementService _tagManagementService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _tagRepositoryMock = _mock.Mock<ITagRepository>();
            _unitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _tagManagementService = _mock.Create<TagManagementService>();
        }

        [TearDown]
        public void TearDown()
        {
            _tagRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task GetPagedTagsAsync_WhenValidParametersProvided_ReturnsPagedTags()
        {
            // Arrange
            const int pageIndex = 1;
            const int pageSize = 10;
            const string searchText = "sample";
            const string sortBy = "tagName";

            var expectedTags = new List<Tag>
            {
                new Tag { Id = Guid.NewGuid(), TagName = "Tag1" },
                new Tag { Id = Guid.NewGuid(), TagName = "Tag2" }
            };
            var expectedTotal = expectedTags.Count;
            var expectedTotalDisplay = expectedTags.Count;

            _unitOfWorkMock.Setup(x => x.TagRepository.GetTableDataAsync(searchText, sortBy, pageIndex, pageSize))
                .ReturnsAsync((expectedTags, expectedTotal, expectedTotalDisplay));

            // Act
            var result = await _tagManagementService.GetPagedTagsAsync(pageIndex, pageSize, searchText, sortBy);

            // Assert
            result.records.ShouldBe(expectedTags);
            result.total.ShouldBe(expectedTotal);
            result.totalDisplay.ShouldBe(expectedTotalDisplay);
        }
    }
}
