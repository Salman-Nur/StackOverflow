using Amazon.S3.Transfer;
using Amazon.S3;
using Autofac;
using AutoMapper;
using Newtonsoft.Json;
using S3.WEB.BucketName;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.Membership;
using System.Text;
using Azure;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.CodeAnalysis;
using Amazon.S3.Model;

namespace StackOverflow.Web.Models
{
    public class ProfileViewModel
    {
        private ILifetimeScope _scope;
        private IProfileManagementService _profileManagementService;
        private IMapper _mapper;

        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? ImageUrl { get; set; }
        public Stream FileStream { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? Questions { get; set; }
        public int? Answers { get; set; }


        public ProfileViewModel() { }

        public ProfileViewModel(IProfileManagementService profileManagementService, 
            IMapper mapper)
        {
            _profileManagementService = profileManagementService;
            _mapper = mapper;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _profileManagementService = _scope.Resolve<IProfileManagementService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public async Task ViewProfileAsync(Guid id)
        {
            User user = await _profileManagementService.GetProfileAsync(id);
            if (user != null)
            {
                _mapper.Map(user, this);
            }
        }

        public async Task<User> ViewProfileAsyncByUserNameAsync(string userName)
        {
            return await _profileManagementService.GetProfileByUserNameAsync(userName);
        }

        public async Task CreateUserAsync(Guid userId, string userName)
        {
            User user = await _profileManagementService.GetProfileAsync(userId);
            if(user == null)
            {
                await _profileManagementService.EditProfileAsync(userId, userName, Title,
                    Description, Country, ImageUrl);
            }
        }

        public async Task GeneratePreSignedUrlAsync(string objectKey)
        {
            using (var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = AWSConstants.bucketName,
                    Key = objectKey,
                    Expires = DateTime.Now.AddHours(1)
                };

                ImageUrl = await client.GetPreSignedURLAsync(request);
            }
        }
    }
}
