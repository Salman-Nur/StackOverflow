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
    public class TagController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<TagController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public TagController(ILifetimeScope scope,
            ILogger<TagController> logger,
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

        [HttpPost]
        public async Task<JsonResult> GetTags(TagListModel model)
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(_scope);
            var data = await model.GetPagedTagsAsync(dataTablesModel);
            return Json(data);
        }
    }
}
