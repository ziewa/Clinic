using System;
using System.Collections.Generic;
using System.Text;
using Clinic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Clinic> Clinics { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Visit> Visits { get; set; }

        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().Property(x => x.UserName).IsRequired();
            base.OnModelCreating(builder);
        }
    }
}
