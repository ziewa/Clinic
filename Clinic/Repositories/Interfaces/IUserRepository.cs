using Clinic.Data;
using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public AppUser GetUserById(string userId);
    }
}
