using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.ViewModels.Home
{
    public class MyVisitsViewModel
    {
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
