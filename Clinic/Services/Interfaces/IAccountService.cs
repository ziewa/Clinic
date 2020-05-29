using Clinic.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Clinic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> Login(LoginViewModel model);
        void Logout();
        Task<IdentityResult> RegisterNewUser(RegisterViewModel model);
    }
}