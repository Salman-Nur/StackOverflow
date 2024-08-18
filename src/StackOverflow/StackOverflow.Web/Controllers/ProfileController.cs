using Amazon.S3.Model;
using Amazon.S3;
using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using S3.WEB.BucketName;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Infrastructure;
using StackOverflow.Infrastructure.Membership;
using StackOverflow.Web.Models;
using System.IO;
using System.Security.Claims;
using static System.Formats.Asn1.AsnWriter;
using System.Text;
using StackOverflow.Domain.Exceptions;
using StackOverflow.Domain.Entities;

namespace StackOverflow.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<ProfileController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(ILifetimeScope scope,
            ILogger<ProfileController> logger,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            var model = _scope.Resolve<ProfileViewModel>();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var id = user.Id;
            var userName = user.UserName;
            await model.CreateUserAsync(id, userName);
            await model.ViewProfileAsync(id);

            if (model.ImageUrl is not null)
            {
                string objectKey = model.ImageUrl;

                await model.GeneratePreSignedUrlAsync(objectKey);
            }
            return View(model);
        }

        public async Task<IActionResult> ViewProfile(string userName)
        {
            var model = _scope.Resolve<ProfileViewModel>();
            var user = await model.ViewProfileAsyncByUserNameAsync(userName);
            if (user == null)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "An error occurred while processing your request. Please try again later.",
                    Type = ResponseTypes.Danger
                });
                return View(model);
            }
            var id = user.Id;

            await model.ViewProfileAsync(id);

            if (model.ImageUrl is not null)
            {
                string objectKey = model.ImageUrl;

                await model.GeneratePreSignedUrlAsync(objectKey);
            }
            return View(model);
        }

        public IActionResult MyQuestions()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetMyQuestions(MyQuestionListModel model)
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(_scope);

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var data = await model.GetPagedMyQuestionsAsync(userId, dataTablesModel);
            return Json(data);
        }

        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var model = _scope.Resolve<ProfileEditModel>();
            await model.LoadAsync(user.Id);
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileEditModel model, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                    model.UserId = user.Id;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string folder = Path.Combine("images/");
                        folder += Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;

                        if (!model.IsImageFile(imageFile.FileName))
                        {
                            ModelState.AddModelError("ImageFile", "Only image files (JPG, JPEG, PNG, SVG) are allowed.");
                            return View(model);
                        }

                        if (imageFile.Length > 250 * 1024)
                        {
                            ModelState.AddModelError("ImageFile", "Profile image must be 250KB or less.");
                            return View(model);
                        }

                        model.ImageUrl = folder;
                        if (! await model.DoesBucketExistAsync(AWSConstants.bucketName))
                        {
                            await model.CreateBucketAsync(AWSConstants.bucketName);
                        }
                        model.UploadFileToS3(imageFile, folder);
                    }
                    await model.EditProfileAsync();
                    if(user.UserName != model.UserName)
                    {
                        user.UserName = model.UserName;
                        await _userManager.UpdateAsync(user);
                    }
                    return RedirectToAction("Index", "Profile");
                }
                catch (DuplicateUserNameException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "An error occurred while processing your request. Please try again later.",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(e, "Server Error");
                }
            }
            return View(model);
        }
    }
}
