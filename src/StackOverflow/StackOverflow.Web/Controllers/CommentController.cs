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
    public class CommentController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CommentController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ILifetimeScope scope,
            ILogger<CommentController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Create(CommentCreateModel model, 
            Guid questionId)
        {
            model.Resolve(_scope);
            var user = await _userManager.GetUserAsync(User);
            model.UserName = user.UserName;
            model.UserId = user.Id;
            if (User.Identity!.IsAuthenticated)
            {
                await model.CreateCommentAsync();
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Comment Posted",
                    Type = ResponseTypes.Success
                });
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
