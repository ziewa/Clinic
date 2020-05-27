using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Clinic.Data;
using Clinic.Models;
using Clinic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Controllers
{
    public class BaseController : Controller 
    {
        private readonly UserManager<AppUser> UserManager;

        private readonly IUserRepository UserRepository;

        public BaseController(UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            UserManager = userManager;
            UserRepository = userRepository;
        }

        protected AppUser GetLoggedUser() =>
            UserRepository.GetUserById(UserManager.GetUserId(HttpContext.User));

        protected IActionResult ErrorPage()
        {
            return View("~/Views/Error.cshtml");
        }
    }

}
