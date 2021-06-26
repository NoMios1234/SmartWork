using System.ComponentModel.DataAnnotations;

namespace SmartWork.Core.ViewModels.CompanyViewModels
{
    public class CompanyViewModel
    {
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company address")]
        public string CompanyAddress { get; set; }

        [Display(Name = "Company phone number")]
        public string CompanyPhoneNumber { get; set; }

        [Display(Name = "About company")]
        public string CompanyDescription { get; set; }

        [Display(Name = "Company photo")]
        public string PhotoFileName { get; set; }
    }
}
