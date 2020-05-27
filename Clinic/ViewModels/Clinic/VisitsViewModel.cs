using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.ViewModels.Clinic
{
    public class VisitsViewModel
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public string Time { get; set; }
    }
}
