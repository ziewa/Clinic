using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }

        public Specialization Specialization { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }
    }
}
