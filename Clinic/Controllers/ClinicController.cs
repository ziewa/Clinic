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
using Clinic.Repositories.Interfaces;
using Clinic.ViewModels.Clinic;
using Clinic.Services.Interfaces;

namespace Clinic.Controllers
{
    [Authorize]
    public class ClinicController : BaseController
    {

        private readonly IClinicService _clinicService;

        public ClinicController(IClinicService clinicService, ILogger<ClinicController> logger,
            UserManager<AppUser> userManager, IUserRepository userRepository) : base(userManager, userRepository)
        {
            _clinicService = clinicService;

        }

        public IActionResult Index()
        {
            var result = _clinicService.GetClinicIndexViewModel();

            if (result.Success)
                return View(result.Data);
            else
                return ErrorPage();
        }
        
        public IActionResult Doctors(int clinicId)
        {
            var result = _clinicService.GetDoctorsViewModel(clinicId);

            if (!result.Success)
                return ErrorPage();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Visits(int doctorId)
        {
            var result = _clinicService.GetVisitsViewModel(doctorId);
            if (!result.Success)
                return ErrorPage();

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Visits(VisitsViewModel model)
        {
            var result = _clinicService.BookVisit(GetLoggedUser(), model.DoctorId, model.Time);

            if (!result.Success)
                return ErrorPage();

            return RedirectToAction(nameof(HomeController.MyVisits), "Home");
        }

        public IActionResult DeleteVisit(int visitId)
        {
            var result = _clinicService.DeleteVisit(visitId);
            if (!result.Success)
                return ErrorPage();

            return RedirectToAction(nameof(HomeController.MyVisits), "Home");
        }


    }
}