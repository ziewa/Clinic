using Bogus;
using Microsoft.AspNetCore.Identity;
using Clinic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Clinic.Data
{
    public class DataInitialization
    {
        private readonly ApplicationDbContext Context;
        private readonly UserManager<AppUser> UserManager;
        private const string PasswordForTestUsers = "Password12345!";

        public DataInitialization(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        private List<string> _specializationNames = new List<string>
        {
            "Ophthalmologist",
            "Dentist",
            "Pediatrician",
            "Oncologist",
            "Cardiologist",
            "Laryngologist",
            "Orthopaedist",
            "Dermatologist",
            "Endocrinologist",
            "Urologist"
        };

        public void SeedUsers()
        {
            if (Context.Users.ToList().Count < 100)
            {
                var address = new Address
                {
                    Country = "Poland",
                    City = "Legnica",
                    Street = "Witelona",
                    HouseNumber = "5A",
                    ZipCode = "59-220"
                };

                Context.Addresses.Add(address);
                Context.SaveChanges();

                var testUser = new AppUser
                {
                    Email = "email@.wp.pl",
                    UserName = "TestUser",
                    FirstName = "Test",
                    LastName = "User",
                    AddressId = address.Id
                };

                testUser.PasswordHash = UserManager.PasswordHasher.HashPassword(testUser, PasswordForTestUsers);
                Context.Users.Add(testUser);
                Context.SaveChanges();

                var userFaker = new Faker<AppUser>()
                     .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                     .RuleFor(u => u.LastName, f => f.Name.LastName())
                     .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                     .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                     .RuleFor(u => u.Address, (f, u) => new Address
                     {
                         Country = f.Address.Country(),
                         City = f.Address.City(),
                         ZipCode = f.Address.ZipCode(),
                         Street = f.Address.StreetName(),
                         HouseNumber = f.Address.BuildingNumber()

                     });

                var users = userFaker.Generate(100);

                foreach (var user in users)
                {
                    var task = UserManager.CreateAsync(user, PasswordForTestUsers);
                    task.Wait();
                }
            }
        }


        public void SeedClinics()
        {
            if (Context.Clinics.ToList().Count < 50)
            {

                var clinicFaker = new Faker<Models.Clinic>()
                     .RuleFor(c => c.Name, f => f.Company.CompanyName())
                     .RuleFor(c => c.Address, (f, c) => new Address
                     {
                         Country = f.Address.Country(),
                         City = f.Address.City(),
                         ZipCode = f.Address.ZipCode(),
                         Street = f.Address.StreetName(),
                         HouseNumber = f.Address.BuildingNumber()

                     });

                var clinics = clinicFaker.Generate(50);

                Context.AddRange(clinics);
                Context.SaveChanges();
            }
        }

        public void SeedSpecializations()
        {
            if (Context.Specializations.ToList().Count < 10)
            {
                List<Specialization> specializations = new List<Specialization>();

                foreach(var specializationName in _specializationNames)
                {
                    specializations.Add(new Specialization
                    {
                        Name = specializationName
                    });
                }


                Context.AddRange(specializations);
                Context.SaveChanges();
            }
        }

        public void SeedDoctors()
        {
            if (Context.Doctors.ToList().Count < 1000)
            {
                var clinicIds = Context.Clinics.Select(x => x.Id).ToList();
                var specializationIds = Context.Specializations.Select(x => x.Id).ToList();

                var doctorFaker = new Faker<Doctor>()
                     .RuleFor(d => d.Name, f => f.Name.FullName())
                     .RuleFor(d => d.ClinicId, f => f.PickRandom<int>(clinicIds))
                     .RuleFor(d => d.SpecializationId, f => f.PickRandom<int>(specializationIds));
                     
                var clinics = doctorFaker.Generate(1000);

                Context.AddRange(clinics);
                Context.SaveChanges();
            }
        }


    }
}
