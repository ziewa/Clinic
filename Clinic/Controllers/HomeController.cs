using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Clinic.Models;
using Clinic.Services;
using Microsoft.AspNetCore.Identity;
using Clinic.Repositories;
using Clinic.Repositories.Interfaces;
using Clinic.Services.Interfaces;
using Clinic.ViewModels;

namespace Clinic.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(UserManager<AppUser> userManager, ILogger<HomeController> logger,
            IHomeService homeService, IUserRepository userRepository) 
            : base(userManager, userRepository)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MyVisits()
        {
            var result = _homeService.GetMyVisitsViewModel(GetLoggedUser());
            if (!result.Success)
                return ErrorPage();

            return View(result.Data);
        }
    }
}
