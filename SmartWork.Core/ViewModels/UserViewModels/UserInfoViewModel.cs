using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Core.ViewModels
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Second name")]
        public string SecondName { get; set; }

        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
