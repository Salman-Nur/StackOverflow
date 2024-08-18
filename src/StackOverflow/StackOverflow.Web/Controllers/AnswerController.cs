using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure;
using StackOverflow.Infrastructure.Membership;
using StackOverflow.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace StackOverflow.Web.Controllers
{
    public class AnswerController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AnswerController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnswerController(ILifetimeScope scope,
            ILogger<AnswerController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AnswerPostRequirementPolicy")]
        public async Task<IActionResult> Create(AnswerCreateModel model, 
            Guid questionId)
        {
            model.Resolve(_scope);
            var user = await _userManager.GetUserAsync(User);
            model.UserName = user.UserName;
            model.UserId = user.Id;
            if (User.Identity!.IsAuthenticated)
            {
                if(model.AnswerText is null)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Please provide a answer",
                        Type = ResponseTypes.Danger
                    });
                }
                else
                {
                    await model.CreateAnswerAsync();
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Answer Posted",
                        Type = ResponseTypes.Success
                    });

                    TempData.Put("ResponseMessage2", new ResponseModel
                    {
                        Message = "Congratulations! You have earned 10 points.",
                        Type = ResponseTypes.Success
                    });
                }
                return RedirectToAction("Details", "Question", new { id = questionId });
            }
            else
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Login Required",
                    Type = ResponseTypes.Danger
                });
                return RedirectToAction("Login", "Auth");
            }
        }
    }
}
