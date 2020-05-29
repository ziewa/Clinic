using Clinic.Models;
using Clinic.Services.ServiceResponses;
using Clinic.ViewModels.Home;

namespace Clinic.Services.Interfaces
{
    public interface IHomeService
    {
        ServiceResponse<MyVisitsViewModel> GetMyVisitsViewModel(AppUser user);
    }
}