using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Autofac;
using AutoMapper;
using Newtonsoft.Json;
using S3.WEB.BucketName;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.Membership;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackOverflow.Web.Models
{
    public class ProfileEditModel
    {
        private ILifetimeScope _scope;
        private IProfileManagementService _profileManagementService;
        private IMapper _mapper;

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserName required")]
        public string UserName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }


        public ProfileEditModel() { }

        public ProfileEditModel(IProfileManagementService profileManagementService, 
            IMapper mapper)
        {
            _profileManagementService = profileManagementService;
            _mapper = mapper;

        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _profileManagementService = _scope.Resolve<IProfileManagementService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public async Task EditProfileAsync()
        {
            await _profileManagementService.EditProfileAsync(UserId, UserName, Title,
                Description, Country, ImageUrl);
        }

        public async Task LoadAsync(Guid id)
        {
            User user = await _profileManagementService.GetProfileAsync(id);
            if (user != null)
            {
                _mapper.Map(user, this);
            }
        }


        public async Task CreateBucketAsync(string bucketName)
        {
            AmazonS3Client client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            PutBucketRequest request = new PutBucketRequest
            {
                BucketName = bucketName
            };

            PutBucketResponse response = await client.PutBucketAsync(request);
        }


        public void UploadFileToS3(IFormFile? imageFile, string fileName)
        {

            AmazonS3Client client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
            var fileTransferUtility = new TransferUtility(client);

            var fileTransferUtilityRequest = new TransferUtilityUploadRequest
            {
                BucketName = AWSConstants.bucketName,
                InputStream = imageFile.OpenReadStream(),
                Key = fileName,
            };

            fileTransferUtility.Upload(fileTransferUtilityRequest);
        }

        public async Task<bool> DoesBucketExistAsync(string bucketName)
        {
            AmazonS3Client client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
            var response = await client.ListBucketsAsync();

            foreach (var bucket in response.Buckets)
            {
                if (bucket.BucketName == bucketName)
                {
                    return true; // Bucket exists
                }
            }

            return false; // Bucket does not exist
        }
        internal bool IsImageFile(string fileName)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".svg" };
            string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }

    }
}
