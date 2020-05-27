using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public AppUser Patient { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        
        public Doctor Doctor { get; set; }

        public DateTime Time { get; set; }
    }
}
