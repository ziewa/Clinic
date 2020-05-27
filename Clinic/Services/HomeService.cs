using Clinic.Data;
using Clinic.Models;
using Clinic.Services.ServiceResponses;
using Clinic.ViewModels.Clinic;
using Clinic.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Services
{
    public class HomeService : BaseService
    {
        public HomeService(ILogger<HomeService> logger, ApplicationDbContext context) : base(logger, context)
        {
        }

        public ServiceResponse<MyVisitsViewModel> GetMyVisitsViewModel(AppUser user)
        {
            var model = new MyVisitsViewModel
            {
                Visits = Context.Visits.Include(x => x.Doctor.Clinic).Include(x => x.Doctor.Specialization)
                .Where(x => x.PatientId == user.Id).ToList()
            };

            return ServiceResponse<MyVisitsViewModel>.Ok(model);

        }
    }
}
