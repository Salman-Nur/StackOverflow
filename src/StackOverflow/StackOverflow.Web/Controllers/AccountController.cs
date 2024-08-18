using Autofac;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using StackOverflow.Infrastructure.Membership;
using StackOverflow.Web.Models;
using System.Text;

namespace StackOverflow.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILifetimeScope _scope;

        public AccountController(ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            ILifetimeScope scope)
        {
            _logger = logger;
            _userManager = userManager;
            _scope = scope;

        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && code != null)
            {
                var decodedTokenByte = WebEncoders.Base64UrlDecode(code);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenByte);
                IdentityResult result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Login", "Auth");
                }
                return RedirectToAction(nameof(EmailConfirmationFailed));
            }

            return RedirectToAction(nameof(EmailConfirmationFailed));
        }
        [AllowAnonymous]
        public IActionResult EmailConfirmationFailed(string code = null)
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> RegisterConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var isverified = await _userManager.IsEmailConfirmedAsync(user);
                if (!isverified)
                {
                    return View();
                }
            }

            return RedirectToAction("Login", "Auth");
        }
    }
}
