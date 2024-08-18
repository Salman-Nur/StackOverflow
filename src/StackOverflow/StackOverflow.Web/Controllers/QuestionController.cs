using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure;
using StackOverflow.Infrastructure.Membership;
using StackOverflow.Web.Models;
using Microsoft.AspNetCore.Authorization;
using StackOverflow.Domain.Exceptions;


namespace StackOverflow.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<QuestionController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionController(ILifetimeScope scope,
            ILogger<QuestionController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "QuestionPostRequirementPolicy")]
        public IActionResult Create()
        {
            var model = _scope.Resolve<QuestionCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "QuestionPostRequirementPolicy")]
        public async Task<IActionResult> Create(QuestionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        TempData.Put("ResponseMessage", new ResponseModel
                        {
                            Message = "Login Required",
                            Type = ResponseTypes.Danger
                        });
                        return RedirectToAction("Login", "Auth");
                    }
                    model.UserId = user.Id;
                    await model.CreateQuestionAsync();
                    await model.CreateTagAsync();

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Question Posted",
                        Type = ResponseTypes.Success
                    });
                    TempData.Put("ResponseMessage2", new ResponseModel
                    {
                        Message = "Congratulations! You have earned 10 points.",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index", "Home");
                }
                catch (DuplicateTitleException de)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in post question",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var model = _scope.Resolve<QuestionDetailsModel>();
            var user = await _userManager.GetUserAsync(User);
            await model.LoadAsync(id);
            await model.LoadCommentAsync(id);
            await model.LoadAnswerAsync(id);
            await model.LoadVoteAsync(id);
            return View(model);
        }


		[HttpPost]
        public async Task<JsonResult> GetQuestions(QuestionListModel model)
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(_scope);
            var data = await model.GetPagedQuestionsAsync(dataTablesModel);
            return Json(data);
        }
    }
}
