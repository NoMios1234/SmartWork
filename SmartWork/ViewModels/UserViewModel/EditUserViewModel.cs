using System.ComponentModel.DataAnnotations;

namespace SmartWork.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
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
