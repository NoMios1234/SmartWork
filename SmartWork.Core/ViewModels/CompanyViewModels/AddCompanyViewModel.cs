using System.ComponentModel.DataAnnotations;

namespace SmartWork.Core.ViewModels
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