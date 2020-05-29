using Clinic.Data;
using Clinic.Models;
using Clinic.Services.Interfaces;
using Clinic.Services.ServiceResponses;
using Clinic.ViewModels.Clinic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Services
{
    public class ClinicService : BaseService, IClinicService
    {
        private readonly ApplicationDbContext _context;

        public ClinicService(ILogger<ClinicService> logger, ApplicationDbContext context) : base(logger, context)
        {
            _context = context;
        }

        public ServiceResponse<ClinicIndexViewModel> GetClinicIndexViewModel()
        {
            var clinics = _context.Clinics.Include(x => x.Address).ToList();

            return ServiceResponse<ClinicIndexViewModel>.Ok(new ClinicIndexViewModel { Clinics = clinics });
        }

        public ServiceResponse<DoctorsViewModel> GetDoctorsViewModel(int clinicId)
        {
            var doctors = _context.Doctors.Include(x => x.Specialization).Where(x => x.ClinicId == clinicId).ToList();

            if (doctors == null)
                return ServiceResponse<DoctorsViewModel>.Error("No doctors found");

            return ServiceResponse<DoctorsViewModel>.Ok(new DoctorsViewModel { Doctors = doctors });
        }

        public ServiceResponse<VisitsViewModel> GetVisitsViewModel(int doctorId)
        {
            return ServiceResponse<VisitsViewModel>.Ok(new VisitsViewModel { DoctorId = doctorId });
        }

        public ServiceResponse<bool> BookVisit(AppUser loggedUser, int doctorId, string time)
        {
            DateTime visitDate = DateTime.ParseExact(time, "yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            var newVisit = new Visit
            {
                DoctorId = doctorId,
                PatientId = loggedUser.Id,
                Time = visitDate
            };

            _context.Add(newVisit);
            _context.SaveChanges();

            return ServiceResponse<bool>.Ok();
        }

        public ServiceResponse<bool> DeleteVisit(int visitId)
        {
            var visit = new Visit { Id = visitId };
            _context.Visits.Attach(visit);
            _context.Visits.Remove(visit);
            _context.SaveChanges();

            return ServiceResponse<bool>.Ok();


        }
    }
}
