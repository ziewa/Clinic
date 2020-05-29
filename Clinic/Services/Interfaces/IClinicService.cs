using Clinic.Models;
using Clinic.Services.ServiceResponses;
using Clinic.ViewModels.Clinic;

namespace Clinic.Services.Interfaces
{
    public interface IClinicService
    {
        ServiceResponse<bool> BookVisit(AppUser loggedUser, int doctorId, string time);
        ServiceResponse<bool> DeleteVisit(int visitId);
        ServiceResponse<ClinicIndexViewModel> GetClinicIndexViewModel();
        ServiceResponse<DoctorsViewModel> GetDoctorsViewModel(int clinicId);
        ServiceResponse<VisitsViewModel> GetVisitsViewModel(int doctorId);
    }
}