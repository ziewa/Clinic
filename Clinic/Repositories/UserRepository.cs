using Clinic.Data;
using Clinic.Models;
using Clinic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext Context { get; }

        public UserRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        AppUser IUserRepository.GetUserById(string userId)
        {
            return Context.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}
