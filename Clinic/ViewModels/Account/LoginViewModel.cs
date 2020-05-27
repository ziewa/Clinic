using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Email or username")]
        public string EmailOrUserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
