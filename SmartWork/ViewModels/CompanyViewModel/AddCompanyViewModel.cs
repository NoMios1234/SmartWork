using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.ViewModels.CompanyViewModel
{
    public class AddCompanyViewModel
    {
        [Required]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Company address")]
        public string CompanyAddress { get; set; }
        [Required]
        [Display(Name = "Company phone number")]
        public string CompanyPhoneNumber { get; set; }
        [Required]
        [Display(Name = "About company")]
        public string CompanyDescription { get; set; }

        [Display(Name = "Company photo")]
        public string PhotoFileName { get; set; }
    }
}
