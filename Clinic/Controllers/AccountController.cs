using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Clinic.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Clinic.Models;
using Clinic.Services;
using Clinic.ViewModels.Account;
using Clinic.Services.Interfaces;
using Clinic.Repositories.Interfaces;

namespace Clinic.Controllers
{
    public class AccountController : BaseController
    {

        private readonly IAccountService _accountService;
        private readonly ILogger Logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger,
            UserManager<AppUser> userManager, IUserRepository userRepository) : base(userManager, userRepository)
        {
            _accountService = accountService;
            Logger = logger;

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterNewUser(model);
                if (!result.Succeeded)
                    AddErrors(result);
                else
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(model);
                if (result.Succeeded)
                {
                    Logger.LogInformation("User logged in.");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                if (result.IsLockedOut)
                {
                    Logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _accountService.Logout();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion

    }
}