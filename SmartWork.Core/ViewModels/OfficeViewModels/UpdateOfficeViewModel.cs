using System.ComponentModel.DataAnnotations;

namespace SmartWork.Core.ViewModels.OfficeViewModels
{
    public class UpdateOfficeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Office name")]
        public string OfficeName { get; set; }

        [Display(Name = "Office address")]
        public string OfficeAddress { get; set; }

        [Display(Name = "Office phone number")]
        public string OfficePhoneNumber { get; set; }

        [Display(Name = "Office's company id")]
        public int CompanyId { get; set; }

        [Display(Name = "Office photo")]
        public string PhotoFileName { get; set; }
    }
}
